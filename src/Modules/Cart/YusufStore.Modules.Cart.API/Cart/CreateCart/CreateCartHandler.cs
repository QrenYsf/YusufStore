using Promotion.Grpc;

namespace YusufStore.Modules.Cart.API.Cart.CreateCart;

public record CreateCartCommand(ShoppingCart Cart) : ICommand<CreateCartResult>;
public record CreateCartResult(string UserName);

public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
{
    public CreateCartCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class CreateCartCommandHandler
    (ICartRepository repository, PromotionProtoService.PromotionProtoServiceClient PromotionProto)
    : ICommandHandler<CreateCartCommand, CreateCartResult>
{
    public async Task<CreateCartResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
    {
        await DeductPromotion(command.Cart, cancellationToken);
        
        await repository.CreateCart(command.Cart, cancellationToken);

        return new CreateCartResult(command.Cart.UserName);
    }

    private async Task DeductPromotion(ShoppingCart cart, CancellationToken cancellationToken)
    {      
        foreach (var item in cart.Items)
        {
            var coupon = await PromotionProto.GetPromotionAsync(new GetPromotionRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }
    }
}
