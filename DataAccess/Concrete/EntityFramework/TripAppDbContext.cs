using Core.Configuration;
using Core.Entities.Concrete;
using Entities.Common;
using Entities.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class TripAppDbContext : IdentityDbContext<User, AppRole, string>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TripAppDbContext).Assembly);
        }
        public override int SaveChanges()
        {
            UpdateTimeStamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateTimeStamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimeStamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity)
                .OfType<BaseEntity>();

            foreach (BaseEntity baseEntity in entities)
            {
                if (baseEntity.CreatedDate == default)
                {
                    baseEntity.CreatedDate = DateTime.UtcNow.AddHours(4);
                }

                baseEntity.UpdatedDate = DateTime.UtcNow.AddHours(4);
            }
        }
    }
}