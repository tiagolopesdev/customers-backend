
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

        public async Task<List<Customer>> GetByName(string name)
        {
            try
            {
                var result = await _collection.FindSync(filter => filter.Name == name).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Guid> UpdateCustomer(Customer entity)
        {
            try
            {
                var filter = Builders<Customer>.Filter
                    .Eq(entityOpt => entityOpt.Id, entity.Id);



                var update = Builders<Customer>.Update
                    .Set(entityOpt => entityOpt.Name, entity.Name)
                    .Set(entityOpt => entityOpt.Payments, entity.Payments)
                    .Set(entityOpt => entityOpt.Buys, entity.Buys)
                    .Set(entityOpt => entityOpt.AmountPaid, entity.AmountPaid)
                    .Set(entityOpt => entityOpt.AmountToPay, entity.AmountToPay)
                    .Set(entityOpt => entityOpt.DateDeleted, entity.DateDeleted)
                    .Set(entityOpt => entityOpt.DateUpdated, DateTime.Now);

                var result = await _collection.UpdateOneAsync(filter, update);

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
