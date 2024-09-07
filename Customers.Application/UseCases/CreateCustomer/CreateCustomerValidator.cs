using Customers.Application.UseCases.CreateUser;
using FluentValidation;

namespace Customers.Application.UseCases.CreateCustomer
{
    public sealed class CreateCustomerValidator : AbstractValidator<CreateCustomerRequest>
    {
        public CreateCustomerValidator()
        {
            RuleFor(property => property.Name).NotEmpty().MinimumLength(3);
        }
    }
}
