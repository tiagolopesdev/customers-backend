using MediatR;

namespace BlockApplication.Contracts.CommandQuery
{
    public interface IMessage<TResponse> : IRequest<TResponse>;
}
