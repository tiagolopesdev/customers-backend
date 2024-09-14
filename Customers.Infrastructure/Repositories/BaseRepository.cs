using Customers.Domain.SeedWork;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
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
                throw new NotImplementedException();
                //var filter = Builders<T>.Filter
                //    .Eq(entityOpt => entityOpt.Id, entity.Id);

                //var update = Builders<T>.Update
                //    .Set(entityOpt => entityOpt., DateTime.Now)
                //    .Set(entityOpt => entityOpt.DateUpdated, DateTime.Now);

                //var result = await _collection.UpdateOneAsync(filter, update);

                //return result.First();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
