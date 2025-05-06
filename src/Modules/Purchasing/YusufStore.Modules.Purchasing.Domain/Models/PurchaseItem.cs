namespace YusufStore.Modules.Purchasing.Domain.Models;
public class PurchaseItem : Entity<PurchaseItemId>
{
    internal PurchaseItem(PurchaseId purchaseId, ProductId productId, int quantity, decimal price)
    {
        Id = PurchaseItemId.Of(Guid.NewGuid());
        PurchaseId = purchaseId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public PurchaseId PurchaseId { get; private set; } = default!;
    public ProductId ProductId { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
}
