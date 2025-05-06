namespace YusufStore.Modules.Purchasing.Domain.Events;

public record PurchaseCreatedEvent(Purchase purchase) : IDomainEvent;
