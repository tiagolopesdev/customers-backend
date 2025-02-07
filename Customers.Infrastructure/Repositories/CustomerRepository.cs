
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Customers.Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _collection;

        public CustomerRepository(IConfiguration configuration) : base(configuration, "customers")
        {
            _collection = new ConnectionDatabase<Customer>(configuration, "customers").InstanceConnection();
        }

        public async Task<List<Customer>> GetByName(string name)
        {
            try
            {
                var result = await _collection.FindSync(filter => filter.Name.Contains(name)).ToListAsync();

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
