using YusufStore.Modules.Purchasing.Domain.Enums;

namespace YusufStore.Modules.Purchasing.Application.Dtos;
public record PurchaseDto(
    Guid Id,
    Guid CustomerId,
    string PurchaseName,
    AddressDto ShippingAddress,
    AddressDto BillingAddress,
    PaymentDto Payment,
    PurchaseStatus Status,
    List<PurchaseItemDto> PurchaseItems);
