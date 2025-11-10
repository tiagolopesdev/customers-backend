using BlockApplication.Contracts.Command;
using FluentValidation;

namespace BlockApplication.Validator;

public class Validator<TRequest> : AbstractValidator<TRequest> where TRequest: ICommand
{
}