namespace YusufStore.Modules.Purchasing.Application.Purchases.Commands.CreatePurchase;
public class CreatePurchaseHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreatePurchaseCommand, CreatePurchaseResult>
{
    public async Task<CreatePurchaseResult> Handle(CreatePurchaseCommand command, CancellationToken cancellationToken)
    {
        var purchase = CreateNewPurchase(command.Purchase);

        dbContext.Purchases.Add(purchase);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreatePurchaseResult(purchase.Id.Value);
    }
    private Purchase CreateNewPurchase(PurchaseDto purchaseDto)
    {
        var shippingAddress = Address.Of(purchaseDto.ShippingAddress.FirstName, purchaseDto.ShippingAddress.LastName, purchaseDto.ShippingAddress.EmailAddress, purchaseDto.ShippingAddress.AddressLine, purchaseDto.ShippingAddress.Country, purchaseDto.ShippingAddress.State, purchaseDto.ShippingAddress.ZipCode);
        var billingAddress = Address.Of(purchaseDto.BillingAddress.FirstName, purchaseDto.BillingAddress.LastName, purchaseDto.BillingAddress.EmailAddress, purchaseDto.BillingAddress.AddressLine, purchaseDto.BillingAddress.Country, purchaseDto.BillingAddress.State, purchaseDto.BillingAddress.ZipCode);

        var newPurchase = Purchase.Create(
                id: PurchaseId.Of(Guid.NewGuid()),
                customerId: CustomerId.Of(purchaseDto.CustomerId),
                purchaseName: PurchaseName.Of(purchaseDto.PurchaseName),
                shippingAddress: shippingAddress,
                billingAddress: billingAddress,
                payment: Payment.Of(purchaseDto.Payment.CardName, purchaseDto.Payment.CardNumber, purchaseDto.Payment.Expiration, purchaseDto.Payment.Cvv, purchaseDto.Payment.PaymentMethod)
                );

        foreach (var purchaseItemDto in purchaseDto.PurchaseItems)
        {
            newPurchase.Add(ProductId.Of(purchaseItemDto.ProductId), purchaseItemDto.Quantity, purchaseItemDto.Price);
        }
        return newPurchase;
    }
}
