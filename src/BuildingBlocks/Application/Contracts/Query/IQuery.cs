using MediatR;

namespace BlockApplication.Contracts.Query;

public interface IQuery<TResult> : IRequest<TResult>
{

}
