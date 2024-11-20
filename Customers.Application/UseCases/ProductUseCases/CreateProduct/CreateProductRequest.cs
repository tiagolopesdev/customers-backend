using MediatR;

namespace Customers.Application.UseCases.ProductUseCases.CreateProduct
{
    public sealed record class CreateProductRequest(string Name, string Description,
        double Value, double BasePrice, int Quantity
        ) : IRequest<Guid>
    {
    }
}