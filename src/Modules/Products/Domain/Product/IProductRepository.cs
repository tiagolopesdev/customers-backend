using BlockDomain.SeedWork;

namespace Product.Domain.Product;

public interface IProductRepository : IBaseRepository<ProductAggregateRoot>
{
  Task<Guid> UpdateProduct(ProductAggregateRoot product);
  Task<List<ProductAggregateRoot>> GetByName(string name);
}
