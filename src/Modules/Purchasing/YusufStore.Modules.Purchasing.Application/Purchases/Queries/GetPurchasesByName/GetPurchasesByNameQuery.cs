namespace YusufStore.Modules.Purchasing.Application.Purchases.Queries.GetPurchasesByName;

public record GetPurchasesByNameQuery(string Name)
    : IQuery<GetPurchasesByNameResult>;

public record GetPurchasesByNameResult(IEnumerable<PurchaseDto> Purchases);