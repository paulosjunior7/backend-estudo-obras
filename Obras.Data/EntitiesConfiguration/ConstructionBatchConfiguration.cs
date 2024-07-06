using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Obras.Data.Entities;
using System;
namespace Obras.Data.EntitiesConfiguration
{
    public class ConstructionBatchConfiguration : IEntityTypeConfiguration<ConstructionBatch>
    {
        public void Configure(EntityTypeBuilder<ConstructionBatch> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.Value).IsRequired();
            builder.Property(p => p.Active).IsRequired();
            builder.Property(p => p.ChangeDate).IsRequired();
            builder.Property(p => p.CreationDate).IsRequired();
            builder.Property(p => p.ConstructionId).IsRequired();
            builder.Property(p => p.PeopleId).IsRequired();

            builder.HasOne(e => e.Construction).WithMany(e => e.ConstructionBatchs).HasForeignKey(e => e.ConstructionId);
            builder.HasOne(e => e.People).WithMany(e => e.ConstructionBatchs).HasForeignKey(e => e.PeopleId).IsRequired(false);
            builder.HasOne(e => e.RegistrationUser).WithMany(e => e.RegistrationConstructionBatchs).HasForeignKey(e => e.RegistrationUserId);
            builder.HasOne(e => e.ChangeUser).WithMany(e => e.ChangeConstructionBatchs).HasForeignKey(e => e.ChangeUserId);
        }
    }
}

