
using Application.Contracts;
using FluentValidation;

namespace Application.Validator;

public class Validator<TRequest> : AbstractValidator<TRequest> where TRequest: ICommand
{
}