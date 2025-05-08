using MassTransit;
namespace YusufStore.Modules.Cart.API.Cart.CheckoutCart;

public class CheckoutCartCommand : ICommand<CheckoutCartResult>
{
    public CartCheckoutDto CartCheckoutDto { get; set; } = default!;
}
public record CheckoutCartResult(bool IsSuccess);

public class CheckoutCartCommandValidator 
    : AbstractValidator<CheckoutCartCommand>
{
    public CheckoutCartCommandValidator()
    {
        RuleFor(x => x.CartCheckoutDto).NotNull().WithMessage("CartCheckoutDto can't be null");  
    }
}

public class CheckoutCartCommandHandler
    (ICartRepository repository,
    IPublishEndpoint publishEndpoint
    )
    : ICommandHandler<CheckoutCartCommand, CheckoutCartResult>
{
    public async Task<CheckoutCartResult> Handle(CheckoutCartCommand command, CancellationToken cancellationToken)
    {      
        var cart = await repository.GetCart(command.CartCheckoutDto.UserName, cancellationToken);
        if (cart == null)
        {
            return new CheckoutCartResult(false);
        }
        var eventMessage = command.CartCheckoutDto.Adapt<CartCheckoutEvent>();
        eventMessage.TotalPrice = cart.TotalPrice;
        eventMessage.UserName = cart.UserName;
        eventMessage.PurchaseItems = cart.Items.Select(item => new PurchaseItemObject
        {
            ProductId = item.ProductId,
            Quantity = item.Quantity,
            Price = item.Price
        }).ToList();

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await repository.DeleteCart(command.CartCheckoutDto.UserName, cancellationToken);

        return new CheckoutCartResult(true);
    }
}
