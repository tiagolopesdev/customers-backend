using MediatR;

namespace Application.Contracts.Query;

public interface IQuery<TResult> : IRequest<TResult>
{

}
