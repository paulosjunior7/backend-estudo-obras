using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Obras.Data.Entities;
using System;
namespace Obras.Data.EntitiesConfiguration
{
    public class ConstructionMaterialConfiguration : IEntityTypeConfiguration<ConstructionMaterial>
    {
        public void Configure(EntityTypeBuilder<ConstructionMaterial> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.PurchaseDate).IsRequired();
            builder.Property(p => p.Quantity).IsRequired();
            builder.Property(p => p.UnitPrice).IsRequired();
            builder.Property(p => p.ConstructionId).IsRequired();
            builder.Property(p => p.ProductId).IsRequired();
            builder.Property(p => p.Active).IsRequired();
            builder.Property(p => p.ChangeDate).IsRequired();
            builder.Property(p => p.CreationDate).IsRequired();
            builder.Property(p => p.ConstructionId).IsRequired();

            builder.HasOne(e => e.Construction).WithMany(e => e.ConstructionMaterials).HasForeignKey(e => e.ConstructionId).IsRequired(false);
            builder.HasOne(e => e.Product).WithMany(e => e.ConstructionMaterials).HasForeignKey(e => e.ProductId).IsRequired(false);
            builder.HasOne(e => e.Group).WithMany(e => e.ConstructionMaterials).HasForeignKey(e => e.GroupId).IsRequired(false);
            builder.HasOne(e => e.Unity).WithMany(e => e.ConstructionMaterials).HasForeignKey(e => e.UnityId).IsRequired(false);
            builder.HasOne(e => e.Provider).WithMany(e => e.ConstructionMaterials).HasForeignKey(e => e.ProviderId).IsRequired(false);
            builder.HasOne(e => e.Brand).WithMany(e => e.ConstructionMaterials).HasForeignKey(e => e.BrandId).IsRequired(false);
            builder.HasOne(e => e.ConstructionInvestor).WithMany(e => e.ConstructionMaterials).HasForeignKey(e => e.ConstructionInvestorId).IsRequired(false);
            builder.HasOne(e => e.RegistrationUser).WithMany(e => e.RegistrationConstructionMaterials).HasForeignKey(e => e.RegistrationUserId);
            builder.HasOne(e => e.ChangeUser).WithMany(e => e.ChangeConstructionMaterials).HasForeignKey(e => e.ChangeUserId);
        }
    }
}

