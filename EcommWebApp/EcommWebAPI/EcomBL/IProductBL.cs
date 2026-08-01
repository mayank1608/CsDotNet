using EcommWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommWebAPI.EcomBL
{
    public interface IProductBL
    {
        public Task<List<Product>> GetAllProducts();
        //        public Task<Product> GetProductById(int id);
        //        public Task<Product> GetProductByName(string name);
        public Task<Product> AddProductDetails(Product product);

        public Task<Product> GetProductById(int id);
        public Task<Product> GetProductByName(string name);

        public Task<Product> RemoveProduct(int id);

        public Task<Product> UpdateProduct(Product product, int id);
        public Task<Product> UpdateProductPrice(double price, int productId);

    }
}
