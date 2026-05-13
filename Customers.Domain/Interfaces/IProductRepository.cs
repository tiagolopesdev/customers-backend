using Customers.Domain.AggregatesModel.Products;
using Customers.Domain.SeedWork;

namespace Customers.Domain.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Guid> UpdateProduct(Product product);
        Task<List<Product>> GetByNameWithoutPagination(string name);
        Task<Pagination<Product>> GetByName(string name, int pageIndex, int pageSize);
    }
}
