using MediatR;

namespace BlockApplication.Contracts.Query;

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
  where TQuery : IQuery<TResult>
{

}
