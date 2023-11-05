using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Obras.Data.Entities;
using System;
namespace Obras.Data.EntitiesConfiguration
{
    public class ConstructionHouseConfiguration : IEntityTypeConfiguration<ConstructionHouse>
    {
        public void Configure(EntityTypeBuilder<ConstructionHouse> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.Description).HasMaxLength(500).IsRequired();
            builder.Property(p => p.FractionBatch);
            builder.Property(p => p.BuildingArea);
            builder.Property(p => p.PermeableArea);
            builder.Property(p => p.Registration).HasMaxLength(20);
            builder.Property(p => p.EnergyConsumptionUnit).HasMaxLength(20);
            builder.Property(p => p.WaterConsumptionUnit).HasMaxLength(20);
            builder.Property(p => p.SaleValue);
            builder.Property(p => p.Active).IsRequired();
            builder.Property(p => p.ChangeDate).IsRequired();
            builder.Property(p => p.CreationDate).IsRequired();
            builder.Property(p => p.ConstructionId).IsRequired();

            builder.HasOne(e => e.Construction).WithMany(e => e.ConstructionHouses).HasForeignKey(e => e.ConstructionId);
            builder.HasOne(e => e.RegistrationUser).WithMany(e => e.RegistrationConstructionHouses).HasForeignKey(e => e.RegistrationUserId);
            builder.HasOne(e => e.ChangeUser).WithMany(e => e.ChangeConstructionHouses).HasForeignKey(e => e.ChangeUserId);
        }
    }
}

