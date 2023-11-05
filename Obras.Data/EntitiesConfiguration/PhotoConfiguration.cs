using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Obras.Data.Entities;
using System;
namespace Obras.Data.EntitiesConfiguration
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.Path).HasMaxLength(200).IsRequired();
            builder.Property(p => p.TypePhoto).IsRequired();
            builder.Property(p => p.ConstrucationId).IsRequired();
            builder.Property(p => p.ChangeDate).IsRequired();
            builder.Property(p => p.CreationDate).IsRequired();

            builder.HasOne(e => e.Construction).WithMany(e => e.Photos).HasForeignKey(e => e.ConstrucationId);
            builder.HasOne(e => e.RegistrationUser).WithMany(e => e.RegistrationPhotos).HasForeignKey(e => e.RegistrationUserId);
            builder.HasOne(e => e.ChangeUser).WithMany(e => e.ChangePhotos).HasForeignKey(e => e.ChangeUserId);
        }
    }
}