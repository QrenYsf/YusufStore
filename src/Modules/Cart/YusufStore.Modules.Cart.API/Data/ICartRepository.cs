namespace YusufStore.Modules.Cart.API.Data;

public interface ICartRepository
{
    Task<ShoppingCart> GetCart(string userName, CancellationToken cancellationToken = default);
    Task<ShoppingCart> CreateCart(ShoppingCart cart, CancellationToken cancellationToken = default);
    Task<bool> DeleteCart(string userName, CancellationToken cancellationToken = default);
}
