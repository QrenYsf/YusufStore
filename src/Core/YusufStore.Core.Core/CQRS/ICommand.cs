using MediatR;

namespace YusufStore.Core.Core.CQRS;

public interface ICommand:ICommand<Unit>
{
}
public interface ICommand<out TResponse>:IRequest<TResponse>
{
}
