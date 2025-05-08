namespace YusufStore.Modules.Purchasing.Application.Purchases.Queries.GetPurchases;
public record GetPurchasesQuery(PaginationRequest PaginationRequest) 
    : IQuery<GetPurchasesResult>;

public record GetPurchasesResult(PaginatedResult<PurchaseDto> Purchases);
