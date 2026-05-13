using Customers.Domain.SeedWork;
using Customers.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Customers.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly IMongoCollection<T> _collection;
        public BaseRepository(IConfiguration configuration, string collection)
        {
            _collection = new ConnectionDatabase<T>(configuration, collection).InstanceConnection();
        }

        public void Create(T entity)
        {
            _collection.InsertOne(entity);
        }

        public Task<Guid> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Pagination<T>> GetAll(int pageIndex, int pageSize)
        {
            try
            {
                var configPagination = ConfigPagination<T>.New(pageIndex, pageSize);

                var resultTest = await _collection.Aggregate()
                    .Match(Builders<T>.Filter.And(configPagination.DefaultFilters))
                    .Facet(configPagination.Count, configPagination.Data)
                    .ToListAsync()
                    ;

                long totalItens = 0;
                List<T> resultData = [];

                if (resultTest[0].Facets[0].Output<AggregateCountResult>().Count > 0)
                {
                    totalItens = resultTest[0].Facets[0].Output<AggregateCountResult>()[0].Count;
                    resultData = resultTest[0].Facets[1].Output<T>().ToList();
                }

                return Pagination<T>.NewPagination(totalItens, resultData, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> GetById(Guid id)
        {
            try
            {
                var result = await _collection.FindSync(filter => filter.Id == id).ToListAsync();

                return result.First();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Guid> Update(T entity)
        {
            try
            {
                await _collection.DeleteOneAsync(element => element.Id == entity.Id);

                await _collection.InsertOneAsync(entity);

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
