using YusufStore.Modules.Purchasing.Application.Purchases.Queries.GetPurchasesByName;

namespace YusufStore.Modules.Purchasing.API.Endpoints;

public record GetPurchasesByNameResponse(IEnumerable<PurchaseDto> Purchases);

public class GetPurchasesByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/purchases/{purchaseName}", async (string purchaseName, ISender sender) =>
        {
            var result = await sender.Send(new GetPurchasesByNameQuery(purchaseName));

            var response = result.Adapt<GetPurchasesByNameResponse>();

            return Results.Ok(response);
        })
        .WithName("GetPurchasesByName")
        .Produces<GetPurchasesByNameResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Purchases By Name")
        .WithDescription("Get Purchases By Name");
    }
}
