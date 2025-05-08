using Promotion.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Promotion.Grpc.Data;
public class PromotionContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    public PromotionContext(DbContextOptions<PromotionContext> options)
       : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, ProductName = "IPhone 16 Pro Max", Description = "IPhone Promotion", Amount = 5000 },
            new Coupon { Id = 2, ProductName = "Samsung Galaxy S25 Ultra", Description = "Samsung Promotion", Amount = 6500 }
            );
    }
}
