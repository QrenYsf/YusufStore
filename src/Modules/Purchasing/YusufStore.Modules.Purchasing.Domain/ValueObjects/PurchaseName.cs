namespace YusufStore.Modules.Purchasing.Domain.ValueObjects;
public record PurchaseName
{  
    public string Value { get; }
    private PurchaseName(string value) => Value = value;
    public static PurchaseName Of(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        return new PurchaseName(value);
    }
}
