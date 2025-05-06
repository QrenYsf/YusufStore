namespace YusufStore.Modules.Purchasing.Application.Purchases.Queries.GetPurchasesByName;
public class GetPurchasesByNameHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetPurchasesByNameQuery, GetPurchasesByNameResult>
{
    public async Task<GetPurchasesByNameResult> Handle(GetPurchasesByNameQuery query, CancellationToken cancellationToken)
    {
        var purchases = await dbContext.Purchases
                .Include(o => o.PurchaseItems)
                .AsNoTracking()
                .Where(o => o.PurchaseName.Value.Contains(query.Name))
                .OrderBy(o => o.PurchaseName.Value)
                .ToListAsync(cancellationToken);                

        return new GetPurchasesByNameResult(purchases.ToPurchaseDtoList());
    }    
}
