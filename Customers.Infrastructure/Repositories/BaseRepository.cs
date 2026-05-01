using Customers.Domain.SeedWork;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Customers.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly IMongoCollection<T> _collection;
        public BaseRepository(IConfiguration configuration, string collection)
        {
            _collection = new ConnectionDatabase<T>(configuration, collection).InstanceConnection();
        }

        public void Create(T entity)
        {
            _collection.InsertOne(entity);
        }

        public Task<Guid> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Pagination<T>> GetAll(int pageIndex, int pageSize)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq(f => f.DateDeleted, null);

                var countFacet = AggregateFacet.Create("count",
                    PipelineDefinition<T, AggregateCountResult>.Create(
                            new[]
                            {
                                PipelineStageDefinitionBuilder.Count<T>()
                            }
                        )
                    );

                var dataFacet = AggregateFacet.Create("data",
                    PipelineDefinition<T, T>.Create(
                            new []
                            {
                                PipelineStageDefinitionBuilder.Sort(
                                    Builders<T>.Sort.Ascending(s => s.DateCreated)
                                    ),
                                PipelineStageDefinitionBuilder.Skip<T>((pageIndex - 1) * pageSize),
                                PipelineStageDefinitionBuilder.Limit<T>(pageSize)
                            }
                        )
                    );

                var resultTest = await _collection.Aggregate()
                    .Match(filter)
                    .Facet(countFacet, dataFacet)
                    .ToListAsync()
                    ;

                var totalItens = resultTest[0].Facets[0].Output<AggregateCountResult>()[0].Count;
                var resultData = resultTest[0].Facets[1].Output<T>().ToList();

                return Pagination<T>.NewPagination(totalItens, resultData, pageIndex, pageSize);
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
