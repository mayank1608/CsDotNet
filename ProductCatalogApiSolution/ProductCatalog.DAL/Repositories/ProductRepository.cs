using ProductCatalog.DAL.Interfaces;
using ProductCatalog.Models;

namespace ProductCatalog.DAL.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductCatalogDbContext _db;

    public ProductRepository(ProductCatalogDbContext db)
    {
        _db = db;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _db.Products
            .Include(x => x.Category)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _db.Products
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.ProductId == id);
    }

    public async Task<Product> AddAsync(Product product)
    {
        await _db.Products.AddAsync(product);

        await _db.SaveChangesAsync();

        return product;
    }

    public async Task<Product?> UpdateAsync(Product product)
    {
        Product? existing =
            await _db.Products.FindAsync(product.ProductId);

        if (existing == null)
            return null;

        existing.ProductName = product.ProductName;
        existing.Price = product.Price;
        existing.Quantity = product.Quantity;
        existing.CategoryId = product.CategoryId;

        await _db.SaveChangesAsync();

        return existing;
    }
}