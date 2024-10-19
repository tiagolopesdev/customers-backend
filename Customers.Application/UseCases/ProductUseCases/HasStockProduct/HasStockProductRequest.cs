
using MediatR;

namespace Customers.Application.UseCases.ProductUseCases.HasStockProduct
{
    public sealed record class HasStockProductRequest(Guid Id) : IRequest<HasStockProductResponse>
    {
    }
}
