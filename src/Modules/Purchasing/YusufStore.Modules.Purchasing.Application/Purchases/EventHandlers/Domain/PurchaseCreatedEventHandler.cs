using MassTransit;
using Microsoft.FeatureManagement;

namespace YusufStore.Modules.Purchasing.Application.Purchases.EventHandlers.Domain;
public class PurchaseCreatedEventHandler
    (
      IPublishEndpoint publishEndpoint,
      IFeatureManager featureManager,
      ILogger<PurchaseCreatedEventHandler> logger)
    : INotificationHandler<PurchaseCreatedEvent>
{
    public async Task Handle(PurchaseCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        if (await featureManager.IsEnabledAsync("PurchaseFullfilment"))
        {
            var purchaseCreatedIntegrationEvent = domainEvent.purchase.ToPurchaseDto();
            await publishEndpoint.Publish(purchaseCreatedIntegrationEvent, cancellationToken);
        }
        //return Task.CompletedTask;
    }
}
