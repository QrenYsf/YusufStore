using System.Security.Claims;

namespace YusufStore.Modules.Cart.API.Cart.DeleteCart;
public record DeleteCartResponse(bool IsSuccess);

public class DeleteCartEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/cart", async (ISender sender, ClaimsPrincipal user) =>
        {
            var userName = user.Identity!.Name;          
            var result = await sender.Send(new DeleteCartCommand(userName));

            var response = result.Adapt<DeleteCartResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteProduct")
        .Produces<DeleteCartResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Product")
        .WithDescription("Delete Product")
        .RequireAuthorization();
    }
}
