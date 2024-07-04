using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Obras.Data.Entities;
using System;
namespace Obras.Data.EntitiesConfiguration
{
    public class ConstructionConfiguration : IEntityTypeConfiguration<Construction>
    {
        public void Configure(EntityTypeBuilder<Construction> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.Identifier).HasMaxLength(50);
            builder.Property(p => p.StatusConstruction);
            builder.Property(p => p.DateBegin);
            builder.Property(p => p.DateEnd);
            builder.Property(p => p.ZipCode).HasMaxLength(10);
            builder.Property(p => p.Address).HasMaxLength(100);
            builder.Property(p => p.Number).HasMaxLength(15);
            builder.Property(p => p.Neighbourhood).HasMaxLength(100);
            builder.Property(p => p.City).HasMaxLength(50);
            builder.Property(p => p.State).HasMaxLength(2);
            builder.Property(p => p.Complement).HasMaxLength(100);
            builder.Property(p => p.BatchArea);
            builder.Property(p => p.BuildingArea);
            builder.Property(p => p.MunicipalRegistration);
            builder.Property(p => p.License);
            builder.Property(p => p.UndergroundUse);
            builder.Property(p => p.Art);
            builder.Property(p => p.Cno);
            builder.Property(p => p.MotherEnrollment);
            builder.Property(p => p.Latitude);
            builder.Property(p => p.Longitude);
            builder.Property(p => p.SaleValue);
            builder.Property(p => p.CompanyId).IsRequired();
            builder.Property(p => p.Active).IsRequired();
            builder.Property(p => p.ChangeDate).IsRequired();
            builder.Property(p => p.CreationDate).HasMaxLength(11);

            builder.HasOne(e => e.Company).WithMany(e => e.Constructions).HasForeignKey(e => e.CompanyId);
            builder.HasOne(e => e.RegistrationUser).WithMany(e => e.RegistrationConstructions).HasForeignKey(e => e.RegistrationUserId);
            builder.HasOne(e => e.ChangeUser).WithMany(e => e.ChangeConstructions).HasForeignKey(e => e.ChangeUserId);
        }
    }
}

