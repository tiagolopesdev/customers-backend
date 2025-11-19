using BlockApplication.Contracts.CommandQuery;
using FluentValidation;

namespace BlockApplication.Validator;

public class Validator<TRequest> : AbstractValidator<TRequest> where TRequest: ICommand<TRequest>
{
}