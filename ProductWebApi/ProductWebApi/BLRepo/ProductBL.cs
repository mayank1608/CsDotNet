using ProductWebApi.DALRepo;
using ProductWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductWebApi.BLRepo
{
    public class ProductBL : IProductBL
    {
        public readonly IProductDALRepo _productDAL;
        public ProductBL(IProductDALRepo productDAL)
        {
            _productDAL = productDAL;
        }
        public async Task<ServiceResult<List<ProductResponse>>> GetAllProductDetails()
        {
            var products = new List<ProductResponse>();

            try
            {
                products = await _productDAL.GetAllProductDetails();
                if (products == null || products.Count == 0)
                {
                    return new ServiceResult<List<ProductResponse>>
                    {
                        Success = false,
                        Message = "No products found"
                    };
                }
                return new ServiceResult<List<ProductResponse>>
                {
                    Success = true,
                    Data = products
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception();
            }
        }

        public async Task<ServiceResult<ProductResponse>> GetProductById(int id)
        {
            try
            {
                var products = await _productDAL.GetProductById(id);
                if (products == null)
                {
                    return new ServiceResult<ProductResponse>
                    {
                        Success = false,
                        Message = "No products found"
                    };
                }
                return new ServiceResult<ProductResponse>
                {
                    Success = true,
                    Data = products
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception();
            }
        }

        public async Task<ServiceResult<Product>> AddProduct(AddProductRequest product)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(product.ProductName))
                    throw new Exception("Product name is required");

                if (product.Price <= 0)
                    throw new Exception("Price must be greater than zero");

                if (product.CategoryId <= 0)
                {
                    throw new Exception("CategoryId is required");
                }

                bool categoryExists = await _productDAL.CategoryExists(product.CategoryId);

                if (!categoryExists)
                {
                    throw new Exception($"Category with Id {product.CategoryId} does not exist");
                }

                var newProduct = await _productDAL.AddProduct(product);
                return new ServiceResult<Product>
                {
                    Success = true,
                    Data = newProduct
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public async Task<ServiceResult<Product>> UpdateProduct(Product product)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(product.ProductName))
                    throw new Exception("Product name is required");

                if (product.Price <= 0)
                    throw new Exception("Price must be greater than zero");

                if (product.CategoryId <= 0)
                {
                    throw new Exception("CategoryId is required");
                }

                bool categoryExists = await _productDAL.CategoryExists(product.CategoryId);

                if (!categoryExists)
                {
                    throw new Exception($"Category with Id {product.CategoryId} does not exist");
                }

                var updatedProduct = await _productDAL.UpdateProduct(product);
                return new ServiceResult<Product>
                {
                    Success = true,
                    Data = updatedProduct
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }

        }
    }
}
