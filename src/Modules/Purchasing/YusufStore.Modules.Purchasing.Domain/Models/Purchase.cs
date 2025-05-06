namespace YusufStore.Modules.Purchasing.Domain.Models;
public class Purchase : Aggregate<PurchaseId>
{
    private readonly List<PurchaseItem> _purchaseItems = new();
    public IReadOnlyList<PurchaseItem> PurchaseItems => _purchaseItems.AsReadOnly();

    public CustomerId CustomerId { get; private set; } = default!;
    public PurchaseName PurchaseName { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public PurchaseStatus Status { get; private set; } = PurchaseStatus.Pending;
    public decimal TotalPrice
    {
        get => PurchaseItems.Sum(x => x.Price * x.Quantity);
        private set { }
    }

    public static Purchase Create(PurchaseId id, CustomerId customerId, PurchaseName purchaseName, Address shippingAddress, Address billingAddress, Payment payment)
    {
        var purchase = new Purchase
        {
            Id = id,
            CustomerId = customerId,
            PurchaseName = purchaseName,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment,
            Status = PurchaseStatus.Pending
        };

        purchase.AddDomainEvent(new PurchaseCreatedEvent(purchase));

        return purchase;
    }

    public void Update(PurchaseName purchaseName, Address shippingAddress, Address billingAddress, Payment payment, PurchaseStatus status)
    {
        PurchaseName = purchaseName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        Status = status;

        AddDomainEvent(new PurchaseUpdatedEvent(this));
    }

    public void Add(ProductId productId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var purchaseItem = new PurchaseItem(Id, productId, quantity, price);
        _purchaseItems.Add(purchaseItem);
    }

    public void Remove(ProductId productId)
    {
        var purchaseItem = _purchaseItems.FirstOrDefault(x => x.ProductId == productId);
        if (purchaseItem is not null)
        {
            _purchaseItems.Remove(purchaseItem);
        }
    }
}
