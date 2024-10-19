using MediatR;

namespace Customers.Application.UseCases.ProductUseCases.GetByNameProduct
{
    public sealed record class GetByNameProductRequest(string? Name) : IRequest<List<GetByNameProductResponse>>
    {
    }
}
