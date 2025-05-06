using MediatR;

namespace YusufStore.Core.Core.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
{
}
