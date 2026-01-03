using BlockDomain.SeedWork;
using BlockInfrastructure.Persistence.Configurations;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace BlockInfrastructure.Persistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : Entity
{
    private readonly IMongoCollection<T> _collection;
    public BaseRepository(IConfiguration configuration, string collection, string module)
    {
        _collection = new DatabaseConnectionFactory<T>(configuration, collection, module).InstanceConnection();
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
