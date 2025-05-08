using Marten.Schema;

namespace YusufStore.Modules.Product.API.Data;

public class ProductInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Entity.Product>().AnyAsync())
            return;

        session.Store<Entity.Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Entity.Product> GetPreconfiguredProducts() => new List<Entity.Product>()
            {
                new Entity.Product()
                {
                    Id = new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
                    Name = "Apple IPhone 16 Pro Max",
                    Description = "6.9 Inch , 120 Hz, 8 GB RAM, 256 GB Storage",
                    ImageFile = "product-1.png",
                    Price = 85000.00M,
                    Category = new List<string> { "Smart Phone" }
                },
                new Entity.Product()
                {
                    Id = new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"),
                    Name = "Samsung Galaxy S25 Ultra",
                    Description = "6.9 Inch , 120 Hz, 12 GB RAM, 256 GB Storage",
                    ImageFile = "product-2.png",
                    Price = 75000.00M,
                    Category = new List<string> { "Smart Phone" }
                },
                new Entity.Product()
                {
                    Id = new Guid("777d6323-e8b1-4bdf-9a75-b0d0d2e7e914"),
                    Name = "Dell Laptop",
                    Description = "1 TB SSD, Intel I7, Geforce RTX",
                    ImageFile = "Dell-png",
                    Price = 50000.00M,
                    Category = new List<string> { "Laptop" }
                },
                new Entity.Product()
                {
                    Id = new Guid("887d6323-e8b1-4bdf-9a75-b0d0d2e7e914"),
                    Name = "Apple Mac Pro Laptop",
                    Description = "1 TB SSD, M2 Ultra",
                    ImageFile = "mac-png",
                    Price = 100000.00M,
                    Category = new List<string> { "Laptop" }
                },
                new Entity.Product()
                {
                    Id = new Guid("127d6323-e8b1-4bdf-9a75-b0d0d2e7e925"),
                    Name = "GeForce RTX 5090",
                    Description = "Top GPU",
                    ImageFile = "5090-png",
                    Price = 150000.00M,
                    Category = new List<string> { "Graphics Card" }
                }
            };

}
