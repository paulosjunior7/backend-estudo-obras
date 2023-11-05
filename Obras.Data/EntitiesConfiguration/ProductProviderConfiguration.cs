using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Obras.Data.Entities;
using System;
namespace Obras.Data.EntitiesConfiguration
{
    public class ProductProviderConfiguration : IEntityTypeConfiguration<ProductProvider>
    {
        public void Configure(EntityTypeBuilder<ProductProvider> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.AuxiliaryCode).HasMaxLength(100);
            builder.Property(p => p.ProductId).IsRequired();
            builder.Property(p => p.ProviderId).IsRequired();
            builder.Property(p => p.Active).IsRequired();
            builder.Property(p => p.ChangeDate).IsRequired();
            builder.Property(p => p.CreationDate).IsRequired();

            builder.HasOne(e => e.Provider).WithMany(e => e.ProductProviders).HasForeignKey(e => e.ProviderId);
            builder.HasOne(e => e.Product).WithMany(e => e.ProductProviders).HasForeignKey(e => e.ProductId);
            builder.HasOne(e => e.RegistrationUser).WithMany(e => e.RegistrationProductProviders).HasForeignKey(e => e.RegistrationUserId);
            builder.HasOne(e => e.ChangeUser).WithMany(e => e.ChangeProductProviders).HasForeignKey(e => e.ChangeUserId);
        }
    }
}