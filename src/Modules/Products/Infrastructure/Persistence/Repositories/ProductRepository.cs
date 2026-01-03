using BlockInfrastructure.Persistence.Configurations;
using BlockInfrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Product.Domain.Product;

namespace Product.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : BaseRepository<ProductAggregateRoot>, IProductRepository
    {
        private readonly IMongoCollection<ProductAggregateRoot> _collection;
        public ProductRepository(IConfiguration configuration) : base(configuration, "product", "Product")
        {
            _collection = new DatabaseConnectionFactory<ProductAggregateRoot>(configuration, "product", "Product").InstanceConnection();
        }
        public async Task<List<ProductAggregateRoot>> GetByName(string name)
        {
            try
            {
                var result = await _collection.FindSync(filter => filter.Name.ToUpper().Contains(name.ToUpper())).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Guid> UpdateProduct(ProductAggregateRoot product)
        {
            try
            {
                var filter = Builders<ProductAggregateRoot>.Filter.Eq(entity => entity.Id, product.Id);

                var update = Builders<ProductAggregateRoot>.Update
                    .Set(entity => entity.Quantity, product.Quantity)
                    .Set(entity => entity.Name, product.Name)
                    .Set(entity => entity.Value, product.Value)
                    .Set(entity => entity.Description, product.Description)
                    .Set(entity => entity.DateUpdated, product.DateUpdated);

                await _collection.UpdateOneAsync(filter, update);

                return product.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}