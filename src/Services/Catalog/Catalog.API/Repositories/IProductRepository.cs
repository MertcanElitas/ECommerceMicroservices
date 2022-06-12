using Catalog.API.Entities;

namespace Catalog.API.Repositories
{
  public interface IProductRepository
  {
    Task<IEnumerable<Product>> GetProducts();

    Task<IEnumerable<Product>> GetProductByName(string name);

    Task<IEnumerable<Product>> GetProductByCategory(string categoryName);

    Task<Product> GetProduct(string id);

    Task<bool> UpdateProduct(Product product);

    Task<bool> DeleteProduct(string id);

    Task CreateProduct(Product product);

  }
}