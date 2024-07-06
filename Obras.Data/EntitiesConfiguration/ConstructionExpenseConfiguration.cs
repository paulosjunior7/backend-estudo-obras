using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Obras.Data.Entities;
using System;
namespace Obras.Data.EntitiesConfiguration
{
    public class ConstructionExpenseConfiguration : IEntityTypeConfiguration<ConstructionExpense>
    {
        public void Configure(EntityTypeBuilder<ConstructionExpense> builder)
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
            builder.Property(p => p.ExpenseId).IsRequired();

            builder.HasOne(e => e.Construction).WithMany(e => e.ConstructionExpenses).HasForeignKey(e => e.ConstructionId);
            builder.HasOne(e => e.Expense).WithMany(e => e.ConstructionExpenses).HasForeignKey(e => e.ExpenseId);
            builder.HasOne(e => e.ConstructionInvestor).WithMany(e => e.ConstructionExpenses).HasForeignKey(e => e.ConstructionInvestorId).IsRequired(false);
            builder.HasOne(e => e.RegistrationUser).WithMany(e => e.RegistrationConstructionExpenses).HasForeignKey(e => e.RegistrationUserId);
            builder.HasOne(e => e.ChangeUser).WithMany(e => e.ChangeConstructionExpenses).HasForeignKey(e => e.ChangeUserId);
        }
    }
}

