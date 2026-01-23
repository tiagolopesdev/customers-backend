using Customers.Domain.SeedWork;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Customers.Infrastructure.Repositories
{
    public class ConnectionDatabase<T> where T : Entity
    {
        private readonly MongoClient _client;
        private readonly IMongoCollection<T> _collection;
        private readonly IConfiguration _configuration;
        private string ConnectionString { get { return _configuration.GetConnectionString("MONGODB_URI"); } }

        public ConnectionDatabase(IConfiguration configuration, string collection)
        {
            _configuration = configuration;
            _client = new MongoClient(ConnectionString);
            _collection = _client.GetDatabase("mini-market-database-26").GetCollection<T>(collection);
        }

        public IMongoCollection<T> InstanceConnection()
        {
            return _collection;
        }
    }
}
