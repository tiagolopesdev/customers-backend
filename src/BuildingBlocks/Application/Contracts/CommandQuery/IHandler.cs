using MediatR;

namespace BlockApplication.Contracts.CommandQuery
{
    public interface IHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> 
        where TRequest : IMessage<TResponse>;
}
