using Customers.Domain.AggregatesModel.Products;
using Customers.Domain.Interfaces;
using Customers.Domain.SeedWork;
using Customers.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
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

        public async Task<Pagination<Product>> GetByName(string name, int pageIndex, int pageSize)
        {
            try
            {
                var configPagination = ConfigPagination<Product>.New(pageIndex, pageSize);

                configPagination.Data.Pipeline.Stages.ToList()
                    .Insert(0, PipelineStageDefinitionBuilder.Sort(
                        Builders<Product>.Sort.Ascending(s => s.Name)
                        )
                    );

                configPagination.DefaultFilters.Add(
                    Builders<Product>.Filter.Regex(r => r.Name, new BsonRegularExpression(name, "i"))
                    );


                var resultTest = await _collection.Aggregate()
                    .Match(Builders<Product>.Filter.And(configPagination.DefaultFilters))
                    .Facet(configPagination.Count, configPagination.Data)
                    .ToListAsync()
                    ;

                long totalItens = 0;
                List<Product> resultData = [];

                if (resultTest[0].Facets[0].Output<AggregateCountResult>().Count > 0)
                {
                    totalItens = resultTest[0].Facets[0].Output<AggregateCountResult>()[0].Count;
                    resultData = resultTest[0].Facets[1].Output<Product>().ToList();
                }

                return Pagination<Product>.NewPagination(totalItens, resultData, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Product>> GetByNameWithoutPagination(string name)
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
