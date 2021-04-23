using Hard.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hard.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.CityName)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(a => a.Complement)                
                .HasColumnType("varchar(250)");

            builder.Property(a => a.Neighborhood)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(a => a.Number)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(a => a.State)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(a => a.Street)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(a => a.ZipCode)
                .IsRequired()
                .HasColumnType("varchar(8)");

            builder.ToTable("Addresses");
        }
    }
}
