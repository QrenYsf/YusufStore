using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace YusufStore.Modules.Cart.API.Data;

public class CachedCartRepository
    (ICartRepository repository, IDistributedCache cache) 
    : ICartRepository
{
    public async Task<ShoppingCart> GetCart(string userName, CancellationToken cancellationToken = default)
    {
        var cachedCart = await cache.GetStringAsync(userName, cancellationToken);
        if (!string.IsNullOrEmpty(cachedCart))
            return JsonSerializer.Deserialize<ShoppingCart>(cachedCart)!;

        var cart = await repository.GetCart(userName, cancellationToken);
        await cache.SetStringAsync(userName, JsonSerializer.Serialize(cart), cancellationToken);
        return cart;
    }

    public async Task<ShoppingCart> CreateCart(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        await repository.CreateCart(cart, cancellationToken);

        await cache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart), cancellationToken);

        return cart;
    }

    public async Task<bool> DeleteCart(string userName, CancellationToken cancellationToken = default)
    {
        await repository.DeleteCart(userName, cancellationToken);

        await cache.RemoveAsync(userName, cancellationToken);

        return true;
    }
}
