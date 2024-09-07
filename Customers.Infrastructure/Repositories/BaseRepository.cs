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

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new MongoClient(ConnectionString);
            _collection = _client.GetDatabase("mini-market-database").GetCollection<T>("customers");
        }

        public void Create(T entity)
        {
            _collection.InsertOne(entity);
        }

        public Task<Guid> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
