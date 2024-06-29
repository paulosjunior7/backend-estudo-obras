using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Obras.Data.Entities;
using System;
namespace Obras.Data.EntitiesConfiguration
{
    public class ConstructionManpowerConfiguration : IEntityTypeConfiguration<ConstructionManpower>
    {
        public void Configure(EntityTypeBuilder<ConstructionManpower> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.EmployeeId);
            builder.Property(p => p.OutsourcedId);
            builder.Property(p => p.Value).IsRequired();
            builder.Property(p => p.ConstructionId).IsRequired();
            builder.Property(p => p.Date).IsRequired();
            builder.Property(p => p.Active).IsRequired();
            builder.Property(p => p.ChangeDate).IsRequired();
            builder.Property(p => p.CreationDate).IsRequired();
            builder.Property(p => p.ConstructionId).IsRequired();

            builder.HasOne(e => e.Employee).WithMany(e => e.ConstructionManpowers).HasForeignKey(e => e.EmployeeId).IsRequired(false);
            builder.HasOne(e => e.Outsourced).WithMany(e => e.ConstructionManpowers).HasForeignKey(e => e.OutsourcedId).IsRequired(false);
            builder.HasOne(e => e.Construction).WithMany(e => e.ConstructionManpowers).HasForeignKey(e => e.ConstructionId);
            builder.HasOne(e => e.RegistrationUser).WithMany(e => e.RegistrationConstructionManpowers).HasForeignKey(e => e.RegistrationUserId);
            builder.HasOne(e => e.ChangeUser).WithMany(e => e.ChangeConstructionManpowers).HasForeignKey(e => e.ChangeUserId);
        }
    }
}

