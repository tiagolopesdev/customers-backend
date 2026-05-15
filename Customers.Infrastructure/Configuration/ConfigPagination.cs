using Customers.Domain.AggregatesModel.CustomerAggregate;
using Customers.Domain.SeedWork;
using MongoDB.Driver;

namespace Customers.Infrastructure.Configuration
{
    public class ConfigPagination<T> where T : Entity
    {
        public AggregateFacet<T, AggregateCountResult>? Count { get; set; }
        public AggregateFacet<T, T>? Data { get; set; }
        public List<FilterDefinition<T>> DefaultFilters { get; set; }

        public static ConfigPagination<T> New(int pageIndex, int pageSize)
        {
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
                        new[]
                        {
                                PipelineStageDefinitionBuilder.Sort(
                                    Builders<T>.Sort.Ascending(s => s.DateCreated)
                                    ),
                                PipelineStageDefinitionBuilder.Skip<T>((pageIndex - 1) * pageSize),
                                PipelineStageDefinitionBuilder.Limit<T>(pageSize)
                        }
                    )
                );

            return new()
            {
                Count = countFacet,
                Data = dataFacet,
                DefaultFilters = new()
                {
                    Builders<T>.Filter.Eq(f => f.DateDeleted, null)
                }
            };
        }
    }
}
