using Mobiliva.Core.Entity;
using Mobiliva.DAL.Entities;
using Mobiliva.DAL.Entities.Orders;
using Mobiliva.Entity.Logs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Mobiliva.Core.Enums.Enums;

namespace Mobiliva.DAL
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

    
            builder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Restrict); 
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            if (ChangeTracker.HasChanges())
            {
                foreach (var item in ChangeTracker.Entries())
                {
                    var temp = (BaseEntity)item.Entity;
                    switch (item.State)
                    {
                        case EntityState.Detached:
                            break;
                        case EntityState.Unchanged:
                            break;
                        case EntityState.Added:
                            temp.RecordStatus = RecordStatus.Active;
                            temp.CreateDate = DateTime.UtcNow;
                            temp.UpdateDate = DateTime.UtcNow;
                            break;
                        case EntityState.Deleted:
                            temp.RecordStatus = RecordStatus.Deleted;
                            temp.UpdateDate = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            temp.UpdateDate = DateTime.UtcNow;
                            break;
                        default:
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}

