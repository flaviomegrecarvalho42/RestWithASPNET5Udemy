using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using RestWithAspNet5Udemy.BLL;
using RestWithAspNet5Udemy.BLL.Interfaces;
using RestWithAspNet5Udemy.Hypermedia.Enricher;
using RestWithAspNet5Udemy.Hypermedia.Filters;
using RestWithAspNet5Udemy.Models.Context;
using RestWithAspNet5Udemy.Repositories;
using RestWithAspNet5Udemy.Repositories.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;

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
            //Add CORS
            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddControllers();

            //Add DataBase Connection (adicionando a conexão com o Banco de Dados)
            var connection = Configuration["MySQLConnection:MySQLConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connection));

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
            services.AddScoped<IPersonBLL, PersonBLL>();
            services.AddScoped<IBookBLL, BookBLL>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
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
                var evolveConnection = new MySqlConnection(connection);
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
