using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
  public class CatalogContext : ICatalogContext
  {
    private readonly IMongoDatabase _database;
    public CatalogContext(IConfiguration configuration)
    {
      var mongoClientSettings = new MongoClientSettings()
      {
        ConnectTimeout = TimeSpan.FromSeconds(3),
        SocketTimeout = TimeSpan.FromSeconds(3),
        //Server = new MongoServerAddress(configuration.GetValue<string>("DatabaseSettings:ConnectionString"))
        Server = new MongoServerAddress("localhost", 27017)
      };

      var client = new MongoClient(mongoClientSettings);
      _database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

      var productCollection = GetMongoCollection<Product>(configuration.GetValue<string>("DatabaseSettings:ProductCollectionName"));

      CatalogContextSeedData.SeedData(productCollection);
    }
    public IMongoCollection<T> GetMongoCollection<T>(string collectionName)
    {
      var collection = _database.GetCollection<T>(collectionName);

      return collection;
    }
  }
}