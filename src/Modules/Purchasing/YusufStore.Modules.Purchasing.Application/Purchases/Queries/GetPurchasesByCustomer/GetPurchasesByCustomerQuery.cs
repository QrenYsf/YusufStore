namespace YusufStore.Modules.Purchasing.Application.Purchases.Queries.GetPurchasesByCustomer;

public record GetPurchasesByCustomerQuery(Guid CustomerId) 
    : IQuery<GetPurchasesByCustomerResult>;

public record GetPurchasesByCustomerResult(IEnumerable<PurchaseDto> Purchases);
