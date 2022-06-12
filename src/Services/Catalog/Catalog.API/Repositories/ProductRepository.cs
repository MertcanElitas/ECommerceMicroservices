using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
  public class ProductRepository : IProductRepository
  {
    private readonly ICatalogContext _context;
    private readonly IMongoCollection<Product> _collection;

    public ProductRepository(ICatalogContext context, IConfiguration configuration)
    {
      _context = context;
      _collection = _context.GetMongoCollection<Product>(configuration.GetValue<string>("DatabaseSettings:ProductCollectionName"));
    }

    public async Task CreateProduct(Product product)
    {
      await _collection.InsertOneAsync(product);
    }

    public async Task<bool> DeleteProduct(string id)
    {
      FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

      DeleteResult deleteResult = await _collection.DeleteOneAsync(filter);

       return deleteResult.IsAcknowledged &&
              deleteResult.DeletedCount > 0;
    }

    public async Task<Product> GetProduct(string id)
    {
      var products = await _collection
                          .Find(x => x.Id == id)
                          .FirstOrDefaultAsync();

      return products;
    }

    public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
    {
      FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

      var products = await _collection
                          .Find(filter)
                          .ToListAsync();

      return products;
    }

    public async Task<IEnumerable<Product>> GetProductByName(string name)
    {
      FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

      var products = await _collection
                          .Find(filter)
                          .ToListAsync();

      return products;
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
      var products = await _collection
                          .Find(x => true)
                          .ToListAsync();

      return products;
    }

    public async Task<bool> UpdateProduct(Product product)
    {
      var updateResult = await _collection.
                                ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

      return updateResult.IsAcknowledged &&
              updateResult.ModifiedCount > 0;
    }
  }
}