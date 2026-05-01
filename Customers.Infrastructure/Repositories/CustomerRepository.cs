
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.Interfaces;
using Customers.Domain.SeedWork;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
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

        public async Task<Pagination<Customer>> GetByName(string name, int pageIndex, int pageSize)
        {
            try
            {
                var filter = Builders<Customer>.Filter.Eq(f => f.DateDeleted, null);

                var countFacet = AggregateFacet.Create("count",
                    PipelineDefinition<Customer, AggregateCountResult>.Create(
                            new[]
                            {
                                PipelineStageDefinitionBuilder.Count<Customer>()
                            }
                        )
                    );

                var dataFacet = AggregateFacet.Create("data",
                    PipelineDefinition<Customer, Customer>.Create(
                            new[]
                            {
                                PipelineStageDefinitionBuilder.Sort(
                                    Builders<Customer>.Sort.Ascending(s => s.Name)
                                    ),
                                PipelineStageDefinitionBuilder.Skip<Customer>((pageIndex - 1) * pageSize),
                                PipelineStageDefinitionBuilder.Limit<Customer>(pageSize)
                            }
                        )
                    );

                var resultTest = await _collection.Aggregate()
                    .Match(filter)
                    .Match(
                    Builders<Customer>.Filter.Regex(
                        r => r.Name,
                        new BsonRegularExpression(name, "i")
                        )
                    )
                    .Facet(countFacet, dataFacet)
                    .ToListAsync()
                    ;

                var totalItens = resultTest[0].Facets[0].Output<AggregateCountResult>()[0].Count;
                var resultData = resultTest[0].Facets[1].Output<Customer>().ToList();

                return Pagination<Customer>.NewPagination(totalItens, resultData, pageIndex, pageSize);
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
