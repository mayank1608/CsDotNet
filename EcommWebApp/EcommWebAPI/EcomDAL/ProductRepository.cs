using EcommWebAPI.EcomBL;
using EcommWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommWebAPI.EcomDAL
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommDbContext _context;
        public List<Product> Products = new();

        public ProductRepository(EcommDbContext db)
        {
            _context = db;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            var products = new List<Product>();
            try
            {
                products = await _context.Products.ToListAsync();
                if (products == null)
                {
                    throw new Exception($"Products not found.");
                }
                return products;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching product: {ex.Message}");
            }
        }


        public async Task<Product> AddProductDetails(Product input_product)
        {
            try
            {
                var product = new Product
                {
                    CategoryId = input_product.CategoryId,
                    Name = input_product.Name,
                    Description = input_product.Description,
                    Price = input_product.Price
                };

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching product: {ex.Message}");
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            try
            {
                var product = await (from p in _context.Products
                                     where p.ProductId == id
                                     select p).FirstOrDefaultAsync();

                if (product == null)
                {
                    throw new Exception($"Product with ID {id} not found.");
                }

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching product: {ex.Message}");
            }
        }

        public async Task<Product> GetProductByName(string name)
        {
            try
            {
                var product = await (from p in _context.Products
                                     where p.Name == name
                                     select p).FirstOrDefaultAsync();

                if (product == null)
                {
                    throw new Exception($"Product with name {name} not found.");
                }

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching product: {ex.Message}");
            }
        }

        public async Task<Product> RemoveProduct(int id)
        {
            try
            {
                var product = await (from p in _context.Products
                                     where p.ProductId == id
                                     select p).FirstOrDefaultAsync();
                if (product == null)
                {
                    throw new Exception($"Product with ID {id} not found.");
                }
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching product: {ex.Message}");
            }
        }

        public async Task<Product> UpdateProduct(Product input_product, int id)
        {
            try
            {
                var desired_product = await (from p in _context.Products
                                     where p.ProductId == id
                                     select p).FirstOrDefaultAsync();

                if (desired_product == null)
                {
                    throw new Exception("Product ID not found.");    
                }
                desired_product.Name = input_product.Name;
                desired_product.Description = input_product.Description;
                desired_product.Price = input_product.Price;
                _context.Products.Update(desired_product);
                await _context.SaveChangesAsync();
                return desired_product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching product: {ex.Message}");
            }
        }

        public async Task<Product> UpdateProductPrice(double price, int productId)
        {
            try
            {
                var desired_product = await (from p in _context.Products
                                     where p.ProductId == productId
                                        select p).FirstOrDefaultAsync();
                if(desired_product == null)
                {
                    throw new Exception("Product ID not found.");
                }
                desired_product.Price = price;
                _context.Products.Update(desired_product);
                await _context.SaveChangesAsync();
                return desired_product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching product: {ex.Message}");
            }
        }

    }
}
