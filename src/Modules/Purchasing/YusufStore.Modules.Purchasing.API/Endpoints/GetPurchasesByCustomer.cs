using YusufStore.Modules.Purchasing.Application.Purchases.Queries.GetPurchasesByCustomer;

namespace YusufStore.Modules.Purchasing.API.Endpoints;
public record GetPurchasesByCustomerResponse(IEnumerable<PurchaseDto> Purchases);

public class GetPurchasesByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/purchases/customer/{customerId}", async (Guid customerId, ISender sender) =>
        {
            var result = await sender.Send(new GetPurchasesByCustomerQuery(customerId));

            var response = result.Adapt<GetPurchasesByCustomerResponse>();

            return Results.Ok(response);
        })
        .WithName("GetPurchasesByCustomer")
        .Produces<GetPurchasesByCustomerResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Purchases By Customer")
        .WithDescription("Get Purchases By Customer");
    }
}
