﻿using Microsoft.EntityFrameworkCore;

namespace RestWithAspNet5Udemy.Models.Context
{
    public class MSSQLContext : DbContext
    {
        public MSSQLContext()
        {

        }

        public MSSQLContext(DbContextOptions<MSSQLContext> options) : base(options) {}

        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
