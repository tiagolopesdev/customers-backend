using Customers.Domain.SeedWork;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Customers.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly MongoClient _client;
        private readonly IMongoCollection<T> _collection;
        private IConfiguration _configuration;
        private string ConnectionString { get { return _configuration.GetConnectionString("MONGODB_URI");  } }

        public BaseRepository(IConfiguration configuration, string collection)
        {
            _configuration = configuration;
            _client = new MongoClient(ConnectionString);
            _collection = _client.GetDatabase("mini-market-database").GetCollection<T>(collection);
        }

        public void Create(T entity)
        {
            _collection.InsertOne(entity);
        }

        public Task<Guid> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAll()
        {
            try
            {
                var result = await _collection.FindSync(filter => filter.DateDeleted == null).ToListAsync();

                return result;
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
