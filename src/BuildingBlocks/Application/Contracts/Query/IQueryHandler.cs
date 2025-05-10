using MediatR;

namespace Application.Contracts.Query;

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
  where TQuery : IQuery<TResult>
{

}
