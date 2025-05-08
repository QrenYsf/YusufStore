namespace YusufStore.Modules.Purchasing.Application.Purchases.Commands.DeletePurchase;
public class DeletePurchaseHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeletePurchaseCommand, DeletePurchaseResult>
{
    public async Task<DeletePurchaseResult> Handle(DeletePurchaseCommand command, CancellationToken cancellationToken)
    {   
        var purchaseId = PurchaseId.Of(command.PurchaseId);
        var purchase = await dbContext.Purchases
            .FindAsync([purchaseId], cancellationToken: cancellationToken);

        if (purchase is null)
        {
            throw new PurchaseNotFoundException(command.PurchaseId);
        }

        dbContext.Purchases.Remove(purchase);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeletePurchaseResult(true);        
    }
}
