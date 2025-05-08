namespace YusufStore.Modules.Purchasing.Application.Purchases.Queries.GetPurchasesByCustomer;
public class GetPurchasesByCustomerHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetPurchasesByCustomerQuery, GetPurchasesByCustomerResult>
{
    public async Task<GetPurchasesByCustomerResult> Handle(GetPurchasesByCustomerQuery query, CancellationToken cancellationToken)
    { 
        var purchases = await dbContext.Purchases
                        .Include(o => o.PurchaseItems)
                        .AsNoTracking()
                        .Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
                        .OrderBy(o => o.PurchaseName.Value)
                        .ToListAsync(cancellationToken);

        return new GetPurchasesByCustomerResult(purchases.ToPurchaseDtoList());        
    }
}
