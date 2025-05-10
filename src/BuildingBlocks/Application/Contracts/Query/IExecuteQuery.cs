
namespace Application.Contracts.Query;

public interface IExecuteQuery
{
  Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
}
