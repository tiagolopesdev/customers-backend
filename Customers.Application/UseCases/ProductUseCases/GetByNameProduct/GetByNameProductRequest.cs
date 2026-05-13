using Customers.Application.Shared.DTO;
using Customers.Domain.AggregatesModel.Products;
using MediatR;

namespace Customers.Application.UseCases.ProductUseCases.GetByNameProduct
{
    public sealed record class GetByNameProductRequest : IRequest<PaginationDto<Product>>
    {
        public string? Name { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
    }
}
