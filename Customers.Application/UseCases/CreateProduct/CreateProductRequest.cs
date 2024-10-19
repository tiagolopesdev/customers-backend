
using MediatR;

namespace Customers.Application.UseCases.CreateProduct
{
    public sealed record class CreateProductRequest(string Name, string Description, double value, int quantity) : IRequest<Guid>
    {
    }
}