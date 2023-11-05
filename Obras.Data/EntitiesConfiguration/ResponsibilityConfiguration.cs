using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Obras.Data.Entities;
using System;
namespace Obras.Data.EntitiesConfiguration
{
    public class ResponsibilityConfiguration : IEntityTypeConfiguration<Responsibility>
    {
        public void Configure(EntityTypeBuilder<Responsibility> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.Description).HasMaxLength(100).IsRequired();
            builder.Property(p => p.CompanyId).IsRequired();
            builder.Property(p => p.Active).IsRequired();
            builder.Property(p => p.ChangeDate).IsRequired();
            builder.Property(p => p.CreationDate).IsRequired();

            builder.HasOne(e => e.Company).WithMany(e => e.Responsibilities).HasForeignKey(e => e.CompanyId);
            builder.HasOne(e => e.RegistrationUser).WithMany(e => e.RegistrationResponsibilities).HasForeignKey(e => e.RegistrationUserId);
            builder.HasOne(e => e.ChangeUser).WithMany(e => e.ChangeResponsibilities).HasForeignKey(e => e.ChangeUserId);
        }
    }
}