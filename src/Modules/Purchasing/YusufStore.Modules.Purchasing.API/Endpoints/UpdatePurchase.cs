using YusufStore.Modules.Purchasing.Application.Purchases.Commands.UpdatePurchase;

namespace YusufStore.Modules.Purchasing.API.Endpoints;

public record UpdatePurchaseRequest(PurchaseDto Purchase);
public record UpdatePurchaseResponse(bool IsSuccess);

public class UpdatePurchase : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/purchases", async (UpdatePurchaseRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdatePurchaseCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdatePurchaseResponse>();

            return Results.Ok(response);
        })
        .WithName("UpdatePurchase")
        .Produces<UpdatePurchaseResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Purchase")
        .WithDescription("Update Purchase");
    }
}
