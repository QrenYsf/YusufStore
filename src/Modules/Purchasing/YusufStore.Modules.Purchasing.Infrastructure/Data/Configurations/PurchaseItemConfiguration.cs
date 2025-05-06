using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YusufStore.Modules.Purchasing.Infrastructure.Data.Configurations;
public class PurchaseItemConfiguration : IEntityTypeConfiguration<PurchaseItem>
{
    public void Configure(EntityTypeBuilder<PurchaseItem> builder)
    {
        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.Id).HasConversion(
                                   purchaseItemId => purchaseItemId.Value,
                                   dbId => PurchaseItemId.Of(dbId));

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);

        builder.Property(oi => oi.Quantity).IsRequired();

        builder.Property(oi => oi.Price).IsRequired();
    }
}
