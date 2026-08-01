using EcommWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EcommWebAPI.EcomDAL
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAllProducts();
        //   public Task<Product> GetProductById(int id);
        // public Task<Product> GetProductByName(string name);
        public Task<Product> AddProductDetails(Product product);
        public Task<Product> GetProductById(int id);
        public Task<Product> GetProductByName(string name);
        public Task<Product> RemoveProduct(int id);
        public Task<Product> UpdateProduct(Product product, int id);
        public Task<Product> UpdateProductPrice(double price,int productId);
    }
}
