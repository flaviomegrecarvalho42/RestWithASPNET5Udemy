using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using RestWithAspNet5Udemy.BLL;
using RestWithAspNet5Udemy.BLL.Interfaces;
using RestWithAspNet5Udemy.Configurations;
using RestWithAspNet5Udemy.Hypermedia.Enricher;
using RestWithAspNet5Udemy.Hypermedia.Filters;
using RestWithAspNet5Udemy.Models.Context;
using RestWithAspNet5Udemy.Repositories;
using RestWithAspNet5Udemy.Repositories.Interfaces;
using RestWithAspNet5Udemy.Services;
using RestWithAspNet5Udemy.Services.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestWithAspNet5Udemy
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Configure the segurity (token)
            var tokenConfigurations = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                Configuration.GetSection("TokenConfiguration"))
                .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            //Configure the parameters authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenConfigurations.Issuer,
                        ValidAudience = tokenConfigurations.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
                    };
                });

            //Configure the parameters authentication
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            //Add CORS
            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddControllers();

            //Add DataBase Connection (adicionando a conexão com o Banco de Dados)
            var connection = Configuration["MSSQLServerConnection:MSSQLServerConnectionString"];
            services.AddDbContext<MSSQLContext>(options => options.UseSqlServer(connection));

            // Execute a migration
            if (Environment.IsDevelopment())
            {
                MigrateDatabase(connection);
            }

            //Implementation Content Negociation
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;

                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            }).AddXmlSerializerFormatters();

            //Add Hateos
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
            filterOptions.ContentResponseEnricherList.Add(new BookEnricher());

            services.AddSingleton(filterOptions);

            //Add Versoning API
            services.AddApiVersioning();

            //Add Swagger in the API
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                             new OpenApiInfo
                             {
                                 Title = "REST API's Fom 0 to Azure with ASP.NET Core 5 and Docker",
                                 Version = "v1",
                                 Description = "API RESTFul developed in course 'REST API's Fom 0 to Azure with ASP.NET Core 5 and Docker'",
                                 Contact = new OpenApiContact
                                 {
                                     Name = "Flavio Carvalho",
                                     Url = new Uri("https://github.com/flaviomegrecarvalho42")
                                 }
                             });
            });

            //Add Dependency Injection (adiciona a injeção de dependência)
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IPersonBLL, PersonBLL>();
            services.AddScoped<IBookBLL, BookBLL>();
            services.AddScoped<ILoginBLL, LoginBLL>();
            services.AddScoped<IFileBLL, FileBLL>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddTransient<ITokenService, TokenService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Enabling the use CORS
            app.UseCors();

            //Generate a Json with a documentation
            app.UseSwagger();

            //Generata a page HTML
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json",
                                  "REST API's Fom 0 to Azure with ASP.NET Core 5 and Docker - v1");
            });

            //Configure a swagger page
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");
            });
        }

        private static void MigrateDatabase(string connection)
        {
            try
            {
                var evolveConnection = new SqlConnection(connection);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "Db/Migrations", "Db/DataSet" },
                    IsEraseDisabled = true
                };

                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed", ex);
                throw;
            }
        }
    }
}
