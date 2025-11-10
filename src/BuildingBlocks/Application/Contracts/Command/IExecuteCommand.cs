namespace BlockApplication.Contracts.Command;

public interface IExecuteCommand
{
  Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);
  Task ExecuteCommandAsync(ICommand command);
}
