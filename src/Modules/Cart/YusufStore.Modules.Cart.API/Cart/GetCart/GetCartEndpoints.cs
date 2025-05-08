using System.Security.Claims;
namespace YusufStore.Modules.Cart.API.Cart.GetCart;
public record GetCartResponse(ShoppingCartDto Cart);

public class GetCartEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/cart", async (ISender sender , ClaimsPrincipal user) =>
        {
            var userName = user.Identity!.Name;           
            var result = await sender.Send(new GetCartQuery(userName));

            var dto = result.Cart.Adapt<ShoppingCartDto>();
            var response = new GetCartResponse(dto);

            return Results.Ok(response);
        })
        .WithName("GetCartByUserName")
        .Produces<GetCartResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Cart By UserName")
        .WithDescription("Get Cart By UserName")
        .RequireAuthorization();
    }
}
