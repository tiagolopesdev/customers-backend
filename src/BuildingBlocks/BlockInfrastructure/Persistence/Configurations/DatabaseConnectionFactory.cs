using BlockDomain.SeedWork;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace BlockInfrastructure.Persistence.Configurations;

public class DatabaseConnectionFactory<T> where T : Entity
{
    private readonly MongoClient _client;
    private readonly IMongoCollection<T> _collection;
    private readonly IConfiguration _configuration;

    public DatabaseConnectionFactory(IConfiguration configuration, string collection, string module)
    {        
        _configuration = configuration;

        var section = _configuration.GetSection(module).GetChildren().ToArray();

        _client = new MongoClient(section[0].Value);
        _collection = _client.GetDatabase(section[1].Value).GetCollection<T>(collection);        
    }

    public IMongoCollection<T> InstanceConnection()
    {
        return _collection;
    }
}
