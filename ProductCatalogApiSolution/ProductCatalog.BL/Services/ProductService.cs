using ProductCatalog.BL.Interfaces;
using ProductCatalog.DAL.Interfaces;
using ProductCatalog.Models;

namespace ProductCatalog.BL.Services;
public class ProductService : IProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public async Task<Product> AddAsync(Product product)
    {
        if (product.Price <= 0)
        {
            throw new Exception(
                "Price should be greater than zero");
        }

        return await _repo.AddAsync(product);
    }

    public async Task<Product?> UpdateAsync(Product product)
    {
        return await _repo.UpdateAsync(product);
    }
}