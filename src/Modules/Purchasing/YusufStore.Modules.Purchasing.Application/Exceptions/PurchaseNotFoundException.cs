namespace YusufStore.Modules.Purchasing.Application.Exceptions;
public class PurchaseNotFoundException : NotFoundException
{
    public PurchaseNotFoundException(Guid id) : base("Purchase", id)
    {
    }
}
