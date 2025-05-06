using System.Security.Claims;

namespace YusufStore.Modules.Cart.API.Cart.CreateCart;

public record CreateCartRequest(ShoppingCart Cart);
public record CreateCartResponse(string UserName);

public class CreateCartEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/cart", async (CreateCartRequest request, ISender sender, ClaimsPrincipal user) =>
        {
            var userName = user.Identity!.Name;            
            var command = request.Adapt<CreateCartCommand>();
            command.Cart.UserName = userName ?? "";

            var result = await sender.Send(command);

            var response = result.Adapt<CreateCartResponse>();

            return Results.Created($"/cart/{response.UserName}", response);
        })
        .WithName("CreateCart")
        .Produces<CreateCartResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Cart")
        .WithDescription("Create Cart")
        .RequireAuthorization();
    }
}
