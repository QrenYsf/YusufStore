using YusufStore.Modules.Purchasing.Application.Purchases.Queries.GetPurchases;

namespace YusufStore.Modules.Purchasing.API.Endpoints;
public record GetPurchasesResponse(PaginatedResult<PurchaseDto> Purchases);

public class GetPurchases : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/purchases", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var result = await sender.Send(new GetPurchasesQuery(request));

            var response = result.Adapt<GetPurchasesResponse>();

            return Results.Ok(response);
        })
        .WithName("GetPurchases")
        .Produces<GetPurchasesResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Purchases")
        .WithDescription("Get Purchases");
    }
}
