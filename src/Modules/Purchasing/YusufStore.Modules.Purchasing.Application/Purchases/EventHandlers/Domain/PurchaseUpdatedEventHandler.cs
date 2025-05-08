namespace YusufStore.Modules.Purchasing.Application.Purchases.EventHandlers.Domain;
public class PurchaseUpdatedEventHandler(ILogger<PurchaseUpdatedEventHandler> logger)
    : INotificationHandler<PurchaseUpdatedEvent>
{
    public Task Handle(PurchaseUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
