using ProductWebApi.Models;

namespace ProductWebApi.BLRepo
{
    public interface IProductBL
    {
        public Task<ServiceResult<List<ProductResponse>>> GetAllProductDetails();
        public Task<ServiceResult<ProductResponse>> GetProductById(int id);
        public Task<ServiceResult<Product>> AddProduct(AddProductRequest product);
        public Task<ServiceResult<Product>> UpdateProduct(Product product);
    }
}
