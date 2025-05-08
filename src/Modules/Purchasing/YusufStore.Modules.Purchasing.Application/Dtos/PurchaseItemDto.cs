namespace YusufStore.Modules.Purchasing.Application.Dtos;

public record PurchaseItemDto(
    Guid PurchaseId, 
    Guid ProductId, 
    int Quantity, 
    decimal Price);
