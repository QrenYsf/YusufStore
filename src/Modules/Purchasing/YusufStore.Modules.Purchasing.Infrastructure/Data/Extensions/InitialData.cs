namespace YusufStore.Modules.Purchasing.Infrastructure.Data.Extensions;
internal class InitialData
{
    public static IEnumerable<Customer> Customers =>
    new List<Customer>
    {
        Customer.Create(CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")), "yusuf", "yusuf@gmail.com"),
        Customer.Create(CustomerId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")), "john", "john@gmail.com")
    };

    public static IEnumerable<Product> Products =>
        new List<Product>
        {
            Product.Create(ProductId.Of(new Guid("5334C996-8457-4CF0-815C-ED2B77C4FF61")), "Apple IPhone 16 Pro Max", 85000),
            Product.Create(ProductId.Of(new Guid("C67D6323-E8B1-4BDF-9A75-B0D0D2E7E914")), "Samsung Galaxy S25 Ultra", 75000),
            Product.Create(ProductId.Of(new Guid("4F136E9F-FF8C-4C1F-9A33-D12F689BDAB8")), "Dell Laptop", 50000),
            Product.Create(ProductId.Of(new Guid("6EC1297B-EC0A-4AA1-BE25-6726E3B51A27")), "Apple Mac Pro Laptop", 100000),
            Product.Create(ProductId.Of(new Guid("5334C996-8457-4CF0-815C-ED2B77C4FF62")), "IPhone 16 Pro Max 512", 70000),
            Product.Create(ProductId.Of(new Guid("A1B2C3D4-0000-1111-2222-333344445555")), "Apple MacBook Pro M2 (14-inch)", 99999),
            Product.Create(ProductId.Of(new Guid("B2C3D4E5-0000-1111-2222-444455556666")), "Sony WH-1000XM5 Wireless Headphones", 7500),
            Product.Create(ProductId.Of(new Guid("EDFF8DB3-FD65-4A2A-872B-AAf4D8219B47")), "Oculus Quest 2 VR Headset", 5500),
            Product.Create(ProductId.Of(new Guid("E5F6A7B8-1A2B-3C4D-5E6F-777788889999")), "Samsung 55-inch QLED 4K TV", 45000),
            Product.Create(ProductId.Of(new Guid("F6A7B8C9-1234-5678-ABCD-888899990000")), "Apple Watch Series 8", 15000),
            Product.Create(ProductId.Of(new Guid("A5FAE76B-5A86-496C-8E64-03EE6726BDC8")), "DJI Mini 3 Pro Drone", 40000)
        };


    public static IEnumerable<Purchase> PurchasesWithItems
    {
        get
        {
            var address1 = Address.Of("yusuf", "q", "yusuf@gmail.com", "Istanbul", "Turkey", "Istanbul", "34000");
            var address2 = Address.Of("john", "doe", "john@gmail.com", "Broadway No:1", "England", "Nottingham", "08050");

            var payment1 = Payment.Of("yusuf", "5555555555554444", "12/28", "355", 1);
            var payment2 = Payment.Of("john", "8885555555554444", "06/30", "222", 2);

            var purchase1 = Purchase.Create(
                            PurchaseId.Of(Guid.NewGuid()),
                            CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")),
                            PurchaseName.Of("ORD_1"),
                            shippingAddress: address1,
                            billingAddress: address1,
                            payment1);
            purchase1.Add(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), 2, 85000);
            purchase1.Add(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), 1, 75000);

            var purchase2 = Purchase.Create(
                            PurchaseId.Of(Guid.NewGuid()),
                            CustomerId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")),
                            PurchaseName.Of("ORD_2"),
                            shippingAddress: address2,
                            billingAddress: address2,
                            payment2);
            purchase2.Add(ProductId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), 1, 50000);
            purchase2.Add(ProductId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), 2, 100000);

            return new List<Purchase> { purchase1, purchase2 };
        }
    }
}
