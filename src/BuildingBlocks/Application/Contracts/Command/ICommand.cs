
using MediatR;

namespace Application.Contracts;

public interface ICommand<TResult> : IRequest<TResult>
{
  Guid Id { get; }
}

public interface ICommand : IRequest
{
  Guid Id { get; }
}