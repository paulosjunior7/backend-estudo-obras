using Microsoft.EntityFrameworkCore;
using ObrasApi.src.Account.Database.Domain;
using ObrasApi.src.Company.Database.Domain;

namespace ObrasApi.src.Shared.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AccountDomain>(entity =>
            {
                entity
                    .HasOne(d => d.Company)
                    .WithMany(d => d.Accounts)
                    .HasForeignKey(d => d.IdCompany)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Fk_Company_Account");
            });
        }

        public DbSet<CompanyDomain> Companies { get; set; }
        public DbSet<AccountDomain> Accounts { get; set; }

    }
}