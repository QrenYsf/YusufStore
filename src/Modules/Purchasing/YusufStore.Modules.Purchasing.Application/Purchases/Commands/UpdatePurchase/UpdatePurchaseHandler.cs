namespace YusufStore.Modules.Purchasing.Application.Purchases.Commands.UpdatePurchase;
public class UpdatePurchaseHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdatePurchaseCommand, UpdatePurchaseResult>
{
    public async Task<UpdatePurchaseResult> Handle(UpdatePurchaseCommand command, CancellationToken cancellationToken)
    {
        var purchaseId = PurchaseId.Of(command.Purchase.Id);
        var purchase = await dbContext.Purchases
            .FindAsync([purchaseId], cancellationToken: cancellationToken);

        if (purchase is null)
        {
            throw new PurchaseNotFoundException(command.Purchase.Id);
        }

        UpdatePurchaseWithNewValues(purchase, command.Purchase);

        dbContext.Purchases.Update(purchase);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdatePurchaseResult(true);        
    }

    public void UpdatePurchaseWithNewValues(Purchase purchase, PurchaseDto purchaseDto)
    {
        var updatedShippingAddress = Address.Of(purchaseDto.ShippingAddress.FirstName, purchaseDto.ShippingAddress.LastName, purchaseDto.ShippingAddress.EmailAddress, purchaseDto.ShippingAddress.AddressLine, purchaseDto.ShippingAddress.Country, purchaseDto.ShippingAddress.State, purchaseDto.ShippingAddress.ZipCode);
        var updatedBillingAddress = Address.Of(purchaseDto.BillingAddress.FirstName, purchaseDto.BillingAddress.LastName, purchaseDto.BillingAddress.EmailAddress, purchaseDto.BillingAddress.AddressLine, purchaseDto.BillingAddress.Country, purchaseDto.BillingAddress.State, purchaseDto.BillingAddress.ZipCode);
        var updatedPayment = Payment.Of(purchaseDto.Payment.CardName, purchaseDto.Payment.CardNumber, purchaseDto.Payment.Expiration, purchaseDto.Payment.Cvv, purchaseDto.Payment.PaymentMethod);

        purchase.Update(
            purchaseName: PurchaseName.Of(purchaseDto.PurchaseName),
            shippingAddress: updatedShippingAddress,
            billingAddress: updatedBillingAddress,
            payment: updatedPayment,
            status: purchaseDto.Status);
    }
}
