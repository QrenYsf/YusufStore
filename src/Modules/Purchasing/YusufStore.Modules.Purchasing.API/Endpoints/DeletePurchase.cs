using YusufStore.Modules.Purchasing.Application.Purchases.Commands.DeletePurchase;

namespace YusufStore.Modules.Purchasing.API.Endpoints;
public record DeletePurchaseResponse(bool IsSuccess);

public class DeletePurchase : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/purchases/{id}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new DeletePurchaseCommand(Id));

            var response = result.Adapt<DeletePurchaseResponse>();

            return Results.Ok(response);
        })
        .WithName("DeletePurchase")
        .Produces<DeletePurchaseResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Purchase")
        .WithDescription("Delete Purchase");
    }
}
