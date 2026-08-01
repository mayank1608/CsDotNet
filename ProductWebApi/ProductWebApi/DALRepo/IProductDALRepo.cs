using ProductWebApi.Models;

namespace ProductWebApi.DALRepo
{
    public interface IProductDALRepo
    {
        public Task<List<ProductResponse>> GetAllProductDetails();
        public Task<ProductResponse> GetProductById(int id);
        public Task<Product> AddProduct(AddProductRequest product);
        public Task<Product> UpdateProduct(Product product);
        public Task<bool> CategoryExists(int id);
    }
}
