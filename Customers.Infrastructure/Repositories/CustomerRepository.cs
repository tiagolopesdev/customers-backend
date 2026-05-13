
using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.Interfaces;
using Customers.Domain.SeedWork;
using Customers.Infrastructure.Configuration;
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

        public async Task<Pagination<Customer>> GetAll(int pageIndex, int pageSize, bool owing)
        {
            try
            {
                var configPagination = ConfigPagination<Customer>.New(pageIndex, pageSize);

                if (owing)
                {
                    configPagination.DefaultFilters.Add(Builders<Customer>.Filter.Where(f => f.AmountToPay > f.AmountPaid));
                }


                var resultTest = await _collection.Aggregate()
                    .Match(Builders<Customer>.Filter.And(configPagination.DefaultFilters))
                    .Facet(configPagination.Count, configPagination.Data)
                    .ToListAsync()
                    ;

                long totalItens = 0;
                List<Customer> resultData = [];

                if (resultTest[0].Facets[0].Output<AggregateCountResult>().Count > 0)
                {
                    totalItens = resultTest[0].Facets[0].Output<AggregateCountResult>()[0].Count;
                    resultData = resultTest[0].Facets[1].Output<Customer>().ToList();
                }

                return Pagination<Customer>.NewPagination(totalItens, resultData, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pagination<Customer>> GetByName(string name, int pageIndex, int pageSize, bool owing)
        {
            try
            {
                var configPagination = ConfigPagination<Customer>.New(pageIndex, pageSize);

                if (owing)
                {
                    configPagination.DefaultFilters.Add(
                        Builders<Customer>.Filter.Where(f => f.AmountToPay > f.AmountPaid)
                        );
                }

                configPagination.Data.Pipeline.Stages.ToList()
                    .Insert(0, PipelineStageDefinitionBuilder.Sort(
                        Builders<Customer>.Sort.Ascending(s => s.Name)
                        )
                    );

                configPagination.DefaultFilters.Add(
                    Builders<Customer>.Filter.Regex(r => r.Name, new BsonRegularExpression(name, "i"))
                    );


                var resultTest = await _collection.Aggregate()
                    .Match(Builders<Customer>.Filter.And(configPagination.DefaultFilters))
                    .Facet(configPagination.Count, configPagination.Data)
                    .ToListAsync()
                    ;

                long totalItens = 0;
                List<Customer> resultData = [];

                if (resultTest[0].Facets[0].Output<AggregateCountResult>().Count > 0)
                {
                    totalItens = resultTest[0].Facets[0].Output<AggregateCountResult>()[0].Count;
                    resultData = resultTest[0].Facets[1].Output<Customer>().ToList();
                }

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
