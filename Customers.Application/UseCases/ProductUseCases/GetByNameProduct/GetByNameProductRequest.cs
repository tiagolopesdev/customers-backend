using Customers.Application.Shared.DTO;
using MediatR;

namespace Customers.Application.UseCases.ProductUseCases.GetByNameProduct
{
    public sealed record class GetByNameProductRequest : IRequest<List<GetByNameProductResponse>>
    {
        public string? Name { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
    }
}
