using FluentValidation;

namespace Customers.Application.UseCases.CustomerUseCases.CreateCustomer
{
    public sealed class CreateCustomerValidator : AbstractValidator<CreateCustomerRequest>
    {
        public CreateCustomerValidator()
        {
            RuleFor(property => property.Name).NotEmpty().MinimumLength(3);
        }
    }
}
