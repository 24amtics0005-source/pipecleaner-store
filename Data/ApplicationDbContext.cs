using Microsoft.EntityFrameworkCore;
using web.net_project.Models;

namespace web.net_project.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Category).IsRequired().HasMaxLength(50);
            entity.Property(x => x.Description).IsRequired().HasMaxLength(500);
            entity.Property(x => x.ImageClass).IsRequired().HasMaxLength(50);
            entity.Property(x => x.Tag).HasMaxLength(30);

        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.CustomerName).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Email).IsRequired().HasMaxLength(200);
            entity.Property(x => x.Address).IsRequired().HasMaxLength(250);
            entity.Property(x => x.City).IsRequired().HasMaxLength(50);
            entity.Property(x => x.PostalCode).IsRequired().HasMaxLength(20);
            entity.Property(x => x.PaymentMethod).IsRequired().HasMaxLength(30);

            entity.HasMany(x => x.Items)
                  .WithOne(x => x.Order)
                  .HasForeignKey(x => x.OrderId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.ProductName).IsRequired().HasMaxLength(100);

        });
    }
}
