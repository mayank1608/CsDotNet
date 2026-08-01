using EcommWebAPI.Models;
using EcommWebAPI.EcomDAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommWebAPI.EcomBL
{
    public class ProductBL : IProductBL
    {
        private readonly IProductRepository _productRepository;
        public ProductBL(IProductRepository productRepository)
        {
            _productRepository = productRepository;

        }
        public async Task<List<Product>> GetAllProducts()
        {

            var productList = new List<Product>();
            try
            {
                productList = await _productRepository.GetAllProducts();
                return productList;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Business Layer: {ex.Message}");
            }
        }

        public async Task<Product> AddProductDetails(Product input_product)
        {
            Product product = new Product();
            try
            {
                product = await _productRepository.AddProductDetails(input_product);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Business Layer: {ex.Message}");
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            Product product = new Product();
            try
            {
                product = await _productRepository.GetProductById(id);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Business Layer: {ex.Message}");
            }
        }

        public async Task<Product> GetProductByName(string name)
        {
            Product product = new Product();
            try
            {
                product = await _productRepository.GetProductByName(name);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Business Layer: {ex.Message}");
            }
        }

        public async Task<Product> RemoveProduct(int id)
        {

            try
            {
                var product = await _productRepository.RemoveProduct(id);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Business Layer: {ex.Message}");
            }
        }

        public async Task<Product> UpdateProduct(Product input_product, int id)
        {

            try
            {
                var product = await _productRepository.UpdateProduct(input_product, id);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Business Layer: {ex.Message}");
            }
        }

        public async Task<Product> UpdateProductPrice(double price, int productId)
        {
            Product product = new Product();
            try
            {
                product = await _productRepository.UpdateProductPrice(price, productId);
                return product;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Business Layer: {ex.Message}");
            }
        }
    }
}
