using Customers.Domain.AggregatesModel.Products;
using Customers.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Customers.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly IMongoCollection<Product> _collection;

        public ProductRepository(IConfiguration configuration) : base(configuration, "product")
        {
            _collection = new ConnectionDatabase<Product>(configuration, "product").InstanceConnection();
        }

        public async Task<List<Product>> GetByName(string name)
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

        public async Task<Guid> UpdateProduct(Product product)
        {
            try
            {
                var filter = Builders<Product>.Filter.Eq(entity => entity.Id, product.Id);

                var update = Builders<Product>.Update
                    .Set(entity => entity.Quantity, product.Quantity)
                    .Set(entity => entity.Name, product.Name)
                    .Set(entity => entity.Value, product.Value)
                    .Set(entity => entity.Description, product.Description)
                    .Set(entity => entity.DateUpdated, product.DateUpdated);

                await _collection.UpdateOneAsync(filter, update);

                return product.Id;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
