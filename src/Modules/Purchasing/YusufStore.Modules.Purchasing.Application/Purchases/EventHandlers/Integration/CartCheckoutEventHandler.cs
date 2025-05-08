using MassTransit;
using YusufStore.Modules.Purchasing.Application.Purchases.Commands.CreatePurchase;

namespace YusufStore.Modules.Purchasing.Application.Purchases.EventHandlers.Integration;
public class CartCheckoutEventHandler
    (ISender sender, ILogger<CartCheckoutEventHandler> logger)
    : IConsumer<CartCheckoutEvent>
{
    public async Task Consume(ConsumeContext<CartCheckoutEvent> context)
    {     
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = MapToCreatePurchaseCommand(context.Message);
        await sender.Send(command);
    }

    private CreatePurchaseCommand MapToCreatePurchaseCommand(CartCheckoutEvent message)
    {   
        var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country, message.State, message.ZipCode);
        var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);
        var purchaseId = Guid.NewGuid();    

        var purchaseItems = message.PurchaseItems.Select(item => new PurchaseItemDto(
            PurchaseId: purchaseId,
            ProductId: item.ProductId,
            Quantity: item.Quantity,
            Price: item.Price
            )).ToList();

        var purchaseDto = new PurchaseDto(
            Id: purchaseId,
            CustomerId: message.CustomerId,
            PurchaseName: message.UserName,
            ShippingAddress: addressDto,
            BillingAddress: addressDto,
            Payment: paymentDto,
            Status: Purchasing.Domain.Enums.PurchaseStatus.Pending,
            PurchaseItems: purchaseItems
            );

        return new CreatePurchaseCommand(purchaseDto);
    }
}
