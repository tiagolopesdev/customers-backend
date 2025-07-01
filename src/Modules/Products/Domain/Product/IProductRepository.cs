using BlockDomain.SeedWork;

namespace Domain.Product;

public interface IProductRepository : IBaseRepository<Product>
{
  Task<Guid> UpdateProduct(Product product);
  Task<List<Product>> GetByName(string name);
}
