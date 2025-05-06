namespace YusufStore.Modules.Cart.API.Dtos
{
    public class ShoppingCartItemDto
    {
        public string ProductId { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
