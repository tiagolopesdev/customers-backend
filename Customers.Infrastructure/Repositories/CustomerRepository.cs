
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Customers.Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly MongoClient _client;
        private readonly IMongoCollection<Customer> _collection;
        private IConfiguration _configuration;
        private string ConnectionString { get { return _configuration.GetConnectionString("MONGODB_URI"); } }

        public CustomerRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _client = new MongoClient(ConnectionString);
            _collection = _client.GetDatabase("mini-market-database").GetCollection<Customer>("customers");
        }

        public Task<List<Customer>> GetByName(string name)
        {
            throw new NotImplementedException();
            //_collection.Find();
            //return new List<Customer>();
        }
    }
}
