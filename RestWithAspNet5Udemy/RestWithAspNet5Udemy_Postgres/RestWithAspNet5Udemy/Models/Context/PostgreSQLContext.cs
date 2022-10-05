using Microsoft.EntityFrameworkCore;

namespace RestWithAspNet5Udemy.Models.Context
{
    public class PostgreSQLContext : DbContext
    {
        public PostgreSQLContext()
        {

        }

        public PostgreSQLContext(DbContextOptions<PostgreSQLContext> options) : base(options) {}

        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
