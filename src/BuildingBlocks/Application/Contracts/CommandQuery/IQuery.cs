using MediatR;

namespace BlockApplication.Contracts.CommandQuery
{
    public interface IQuery<TRequest> : IMessage<TRequest>;
}
