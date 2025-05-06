namespace YusufStore.Modules.Purchasing.Application.Purchases.Commands.CreatePurchase;

public record CreatePurchaseCommand(PurchaseDto Purchase)
    : ICommand<CreatePurchaseResult>;

public record CreatePurchaseResult(Guid Id);

public class CreatePurchaseCommandValidator : AbstractValidator<CreatePurchaseCommand>
{
    public CreatePurchaseCommandValidator()
    {
        RuleFor(x => x.Purchase.PurchaseName).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Purchase.CustomerId).NotNull().WithMessage("CustomerId is required");
        RuleFor(x => x.Purchase.PurchaseItems).NotEmpty().WithMessage("PurchaseItems should not be empty");
    }
}