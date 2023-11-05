using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Obras.Data.Entities;
using System;
namespace Obras.Data.EntitiesConfiguration
{
    public class ConstructionAdvanceMoneyConfiguration : IEntityTypeConfiguration<ConstructionAdvanceMoney>
    {
        public void Configure(EntityTypeBuilder<ConstructionAdvanceMoney> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.Date).IsRequired();
            builder.Property(p => p.Value).IsRequired();
            builder.Property(p => p.Active).IsRequired();
            builder.Property(p => p.ChangeDate).IsRequired();
            builder.Property(p => p.CreationDate);
            builder.Property(p => p.ConstructionId).IsRequired();
            builder.Property(p => p.ConstructionInvestorId).IsRequired();

            builder.HasOne(e => e.Construction).WithMany(e => e.ConstructionAdvanceMoneys).HasForeignKey(e => e.ConstructionId);
            builder.HasOne(e => e.ConstructionInvestor).WithMany(e => e.ConstructionAdvanceMoneys).HasForeignKey(e => e.ConstructionInvestorId);
            builder.HasOne(e => e.RegistrationUser).WithMany(e => e.RegistrationConstructionAdvanceMoneys).HasForeignKey(e => e.RegistrationUserId);
            builder.HasOne(e => e.ChangeUser).WithMany(e => e.ChangeConstructionAdvanceMoneys).HasForeignKey(e => e.ChangeUserId);
        }
    }
}

