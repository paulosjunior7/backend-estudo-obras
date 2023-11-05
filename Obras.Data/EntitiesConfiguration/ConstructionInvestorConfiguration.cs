using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Obras.Data.Entities;
using System;
namespace Obras.Data.EntitiesConfiguration
{
    public class ConstructionInvestorConfiguration : IEntityTypeConfiguration<ConstructionInvestor>
    {
        public void Configure(EntityTypeBuilder<ConstructionInvestor> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.ConstructionId).IsRequired();
            builder.Property(p => p.PeopleId).IsRequired();
            builder.Property(p => p.Active).IsRequired();
            builder.Property(p => p.ChangeDate).IsRequired();
            builder.Property(p => p.CreationDate).IsRequired();
            builder.Property(p => p.ConstructionId).IsRequired();

            builder.HasOne(e => e.Construction).WithMany(e => e.ConstructionInvestors).HasForeignKey(e => e.ConstructionId);
            builder.HasOne(e => e.People).WithMany(e => e.ConstructionInvestors).HasForeignKey(e => e.PeopleId);
            builder.HasOne(e => e.RegistrationUser).WithMany(e => e.RegistrationConstructionInvestors).HasForeignKey(e => e.RegistrationUserId);
            builder.HasOne(e => e.ChangeUser).WithMany(e => e.ChangeConstructionInvestors).HasForeignKey(e => e.ChangeUserId);
        }
    }
}

