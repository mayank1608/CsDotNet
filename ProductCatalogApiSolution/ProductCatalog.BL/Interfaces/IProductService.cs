using ProductCatalogApiSolution.Models;

namespace ProductCatalog.BL.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();

    Task<Product?> GetByIdAsync(int id);

    Task<Product> AddAsync(Product product);

    Task<Product?> UpdateAsync(Product product);
}