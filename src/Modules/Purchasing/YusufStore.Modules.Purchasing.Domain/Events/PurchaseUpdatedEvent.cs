namespace YusufStore.Modules.Purchasing.Domain.Events;
public record PurchaseUpdatedEvent(Purchase purchase) : IDomainEvent;
