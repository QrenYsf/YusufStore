namespace YusufStore.Modules.Purchasing.Application.Purchases.Commands.DeletePurchase;

public record DeletePurchaseCommand(Guid PurchaseId)
    : ICommand<DeletePurchaseResult>;

public record DeletePurchaseResult(bool IsSuccess);

public class DeletePurchaseCommandValidator : AbstractValidator<DeletePurchaseCommand>
{
    public DeletePurchaseCommandValidator()
    {
        RuleFor(x => x.PurchaseId).NotEmpty().WithMessage("PurchaseId is required");
    }
}
