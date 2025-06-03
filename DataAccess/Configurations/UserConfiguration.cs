using Core.Entities.Concrete;
using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(u => u.FirstName)
                   .HasMaxLength(100);

            builder.Property(u => u.LastName)
                   .HasMaxLength(100);

            PasswordHasher<User> hasher = new PasswordHasher<User>();

            User admin = new User
            {
                Id = "43B64316-DE56-4300-AE86-C298AEA73C7B",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "Admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PhoneNumberConfirmed = true
            };
            admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");

            User customer = new User
            {
                Id = "99D74F43-7E23-41BE-9F98-10C5D6130312",
                UserName = "customer",
                NormalizedUserName = "CUSTOMER",
                Email = "Customer@gmail.com",
                NormalizedEmail = "CUSTOMER@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PhoneNumberConfirmed = true
            };
            customer.PasswordHash = hasher.HashPassword(customer, "Customer123!");

            builder.HasData(admin, customer);
        }
    }
}
