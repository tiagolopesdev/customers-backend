using AutoMapper;
using Customers.Domain.AggregatesModel.Products;

namespace Customers.Application.UseCases.ProductUseCases.UpdateProduct
{
    public class UpdateProductMapper : Profile
    {
        public UpdateProductMapper()
        {
            CreateMap<UpdateProductRequest, Product>();
        }
    }
}
