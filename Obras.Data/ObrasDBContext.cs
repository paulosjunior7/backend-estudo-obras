using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Obras.Data.Entities;
using System;

namespace Obras.Data
{
    public class ObrasDBContext : IdentityDbContext
    {
        private static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddConsole(); // Você pode adicionar outros provedores de log, se necessário
    });

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ObrasDBContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
            base.OnConfiguring(optionsBuilder);
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
        public DbSet<Outsourced> Outsourseds { get; set; }
        public DbSet<ProductProvider> ProductProviders { get; set; }
        public DbSet<Construction> Constructions { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<ConstructionInvestor> ConstructionInvestors { get; set; }
        public DbSet<ConstructionBatch> ConstructionBatchs { get; set; }
        public DbSet<ConstructionHouse> ConstructionHouses { get; set; }
        public DbSet<Unity> Unities { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<ConstructionMaterial> ConstructionMaterials { get; set; }
        public DbSet<ConstructionManpower> ConstructionManpowers { get; set; }
        public DbSet<ConstructionDocumentation> ConstructionDocumentations { get; set; }
        public DbSet<ConstructionExpense> ConstructionExpenses { get; set; }
        public DbSet<ConstructionAdvanceMoney> ConstructionAdvancesMoney { get; set; }
    }
}
