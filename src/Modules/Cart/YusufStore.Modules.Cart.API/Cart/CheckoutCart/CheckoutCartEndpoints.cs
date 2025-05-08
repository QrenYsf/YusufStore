using System.Security.Claims;

namespace YusufStore.Modules.Cart.API.Cart.CheckoutCart;

public record CheckoutCartRequest(CartCheckoutDto CartCheckoutDto);
public record CheckoutCartResponse(bool IsSuccess);

public class CheckoutCartEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/cart/checkout", async (CheckoutCartRequest request, ISender sender, ClaimsPrincipal user) =>
        {
            var userName = user.Identity!.Name;
            var command = request.Adapt<CheckoutCartCommand>();
            //command.CartCheckoutDto = new CartCheckoutDto();
            command.CartCheckoutDto.UserName= userName ?? "";

            var result = await sender.Send(command);

            var response = result.Adapt<CheckoutCartResponse>();

            return Results.Ok(response);
        })
        .WithName("CheckoutCart")
        .Produces<CheckoutCartResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Checkout Cart")
        .WithDescription("Checkout Cart")
        .RequireAuthorization();
    }
}
