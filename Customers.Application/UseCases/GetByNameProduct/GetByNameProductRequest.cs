using MediatR;

namespace Customers.Application.UseCases.GetByNameProduct
{
    public sealed record class GetByNameProductRequest(string Name) : IRequest<List<GetByNameProductResponse>>
    {
    }
}
