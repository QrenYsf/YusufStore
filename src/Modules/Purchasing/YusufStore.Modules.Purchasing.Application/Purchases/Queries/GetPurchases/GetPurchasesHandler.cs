namespace YusufStore.Modules.Purchasing.Application.Purchases.Queries.GetPurchases;
public class GetPurchasesHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetPurchasesQuery, GetPurchasesResult>
{
    public async Task<GetPurchasesResult> Handle(GetPurchasesQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Purchases.LongCountAsync(cancellationToken);

        var purchases = await dbContext.Purchases
                       .Include(o => o.PurchaseItems)
                       .OrderBy(o => o.PurchaseName.Value)
                       .Skip(pageSize * pageIndex)
                       .Take(pageSize)
                       .ToListAsync(cancellationToken);

        return new GetPurchasesResult(
            new PaginatedResult<PurchaseDto>(
                pageIndex,
                pageSize,
                totalCount,
                purchases.ToPurchaseDtoList()));        
    }
}
