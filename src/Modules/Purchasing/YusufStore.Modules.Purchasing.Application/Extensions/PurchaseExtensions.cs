namespace YusufStore.Modules.Purchasing.Application.Extensions;
public static class PurchaseExtensions
{
    public static IEnumerable<PurchaseDto> ToPurchaseDtoList(this IEnumerable<Purchase> purchases)
    {
        return purchases.Select(purchase => new PurchaseDto(
            Id: purchase.Id.Value,
            CustomerId: purchase.CustomerId.Value,
            PurchaseName: purchase.PurchaseName.Value,
            ShippingAddress: new AddressDto(purchase.ShippingAddress.FirstName, purchase.ShippingAddress.LastName, purchase.ShippingAddress.EmailAddress!, purchase.ShippingAddress.AddressLine, purchase.ShippingAddress.Country, purchase.ShippingAddress.State, purchase.ShippingAddress.ZipCode),
            BillingAddress: new AddressDto(purchase.BillingAddress.FirstName, purchase.BillingAddress.LastName, purchase.BillingAddress.EmailAddress!, purchase.BillingAddress.AddressLine, purchase.BillingAddress.Country, purchase.BillingAddress.State, purchase.BillingAddress.ZipCode),
            Payment: new PaymentDto(purchase.Payment.CardName!, purchase.Payment.CardNumber, purchase.Payment.Expiration, purchase.Payment.CVV, purchase.Payment.PaymentMethod),
            Status: purchase.Status,
            PurchaseItems: purchase.PurchaseItems.Select(oi => new PurchaseItemDto(oi.PurchaseId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
        ));
    }

    public static PurchaseDto ToPurchaseDto(this Purchase purchase)
    {
        return DtoFromPurchase(purchase);
    }

    private static PurchaseDto DtoFromPurchase(Purchase purchase)
    {
        return new PurchaseDto(
                    Id: purchase.Id.Value,
                    CustomerId: purchase.CustomerId.Value,
                    PurchaseName: purchase.PurchaseName.Value,
                    ShippingAddress: new AddressDto(purchase.ShippingAddress.FirstName, purchase.ShippingAddress.LastName, purchase.ShippingAddress.EmailAddress!, purchase.ShippingAddress.AddressLine, purchase.ShippingAddress.Country, purchase.ShippingAddress.State, purchase.ShippingAddress.ZipCode),
                    BillingAddress: new AddressDto(purchase.BillingAddress.FirstName, purchase.BillingAddress.LastName, purchase.BillingAddress.EmailAddress!, purchase.BillingAddress.AddressLine, purchase.BillingAddress.Country, purchase.BillingAddress.State, purchase.BillingAddress.ZipCode),
                    Payment: new PaymentDto(purchase.Payment.CardName!, purchase.Payment.CardNumber, purchase.Payment.Expiration, purchase.Payment.CVV, purchase.Payment.PaymentMethod),
                    Status: purchase.Status,
                    PurchaseItems: purchase.PurchaseItems.Select(oi => new PurchaseItemDto(oi.PurchaseId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
                );
    }
}
