using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.FromCity)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(o => o.ToCity)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(o => o.Description)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(o => o.TripDate)
                   .IsRequired();

            builder.Property(o => o.Status)
                   .IsRequired();

            builder.HasOne(o => o.User)
                   .WithMany(u => u.Orders)
                   .HasForeignKey(o => o.UserId)
                   .OnDelete(DeleteBehavior.SetNull); 
        }
    }
}
