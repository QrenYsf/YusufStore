namespace YusufStore.Modules.Purchasing.Application.Purchases.Commands.UpdatePurchase;
public record UpdatePurchaseCommand(PurchaseDto Purchase)
    : ICommand<UpdatePurchaseResult>;

public record UpdatePurchaseResult(bool IsSuccess);

public class UpdatePurchaseCommandValidator : AbstractValidator<UpdatePurchaseCommand>
{
    public UpdatePurchaseCommandValidator()
    {
        RuleFor(x => x.Purchase.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Purchase.PurchaseName).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Purchase.CustomerId).NotNull().WithMessage("CustomerId is required");
    }
}

