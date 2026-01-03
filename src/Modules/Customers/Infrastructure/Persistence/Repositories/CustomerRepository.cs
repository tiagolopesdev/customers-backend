
using Microsoft.Extensions.Configuration;
using BlockInfrastructure.Persistence.Repositories;
using MongoDB.Driver;
using Domain.Customers;
using BlockInfrastructure.Persistence.Configurations;

namespace Infrastructure.Persistence.Repositories;

public class CustomerRepository : BaseRepository<CustomerAggregateRoot>, ICustomerRepository
{
    private readonly IMongoCollection<CustomerAggregateRoot> _collection;
    public CustomerRepository(IConfiguration configuration) : base(configuration, "customers", "Customer")
    {
        _collection = new DatabaseConnectionFactory<CustomerAggregateRoot>(configuration, "customers", "Customer").InstanceConnection();
    }

    public async Task<List<CustomerAggregateRoot>> GetByName(string name)
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
    public async Task<Guid> UpdateCustomer(CustomerAggregateRoot entity)
    {
        try
        {
            var filter = Builders<CustomerAggregateRoot>.Filter
                .Eq(entityOpt => entityOpt.Id, entity.Id);



            var update = Builders<CustomerAggregateRoot>.Update
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
