using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Obras.Data.Entities;
using System;

namespace Obras.Data
{
    public class ObrasDBContext : IdentityDbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public ObrasDBContext(DbContextOptions<ObrasDBContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Documentation> Documentations { get; set; }
        public DbSet<Responsibility> Responsibilities { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<People> Peoples { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
