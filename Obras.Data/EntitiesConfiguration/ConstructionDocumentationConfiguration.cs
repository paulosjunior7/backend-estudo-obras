using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Obras.Data.Entities;
using System;
namespace Obras.Data.EntitiesConfiguration
{
    public class ConstructionDocumentationConfiguration : IEntityTypeConfiguration<ConstructionDocumentation>
    {
        public void Configure(EntityTypeBuilder<ConstructionDocumentation> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.Value).IsRequired();
            builder.Property(p => p.Date).IsRequired();
            builder.Property(p => p.Active).IsRequired();
            builder.Property(p => p.ChangeDate).IsRequired();
            builder.Property(p => p.CreationDate).IsRequired();
            builder.Property(p => p.ConstructionId).IsRequired();
            builder.Property(p => p.ConstructionInvestorId);
            builder.Property(p => p.DocumentationId).IsRequired();

            builder.HasOne(e => e.Construction).WithMany(e => e.ConstructionDocumentations).HasForeignKey(e => e.ConstructionId);
            builder.HasOne(e => e.Documentation).WithMany(e => e.ConstructionDocumentations).HasForeignKey(e => e.DocumentationId);
            builder.HasOne(e => e.ConstructionInvestor).WithMany(e => e.ConstructionDocumentations).HasForeignKey(e => e.ConstructionInvestorId).IsRequired(false);
            builder.HasOne(e => e.RegistrationUser).WithMany(e => e.RegistrationConstructionDocumentations).HasForeignKey(e => e.RegistrationUserId);
            builder.HasOne(e => e.ChangeUser).WithMany(e => e.ChangeConstructionDocumentations).HasForeignKey(e => e.ChangeUserId);
        }
    }
}

