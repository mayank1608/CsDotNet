using ProductWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductWebApi.DALRepo
{
    public class ProductDALRepo : IProductDALRepo
    {
        public readonly ECommDBContext dBContext;
        public ProductDALRepo(ECommDBContext context)
        {
            // dBContext = new ECommDBContext();
            dBContext = context;
        }
        // public async Task<List<ProductResponse>> GetAllProductDetails()
        // {
        //     var products = new List<ProductResponse>();

        //     try
        //     {
        //         // products = await dBContext.Products.ToListAsync();
        //         products = await dBContext.Products.Include(x => x.Category).ToListAsync();
        //         return products;
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine(ex.Message);
        //         throw new Exception();
        //     }
        // }

        public async Task<List<ProductResponse>> GetAllProductDetails()
        {
            var products = new List<ProductResponse>();
            try
            {
                products = await dBContext.Products
                .Select(x => new ProductResponse
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Price = x.Price,
                    Description = x.Description,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category!.CategoryName
                })
                .ToListAsync();
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception();
            }

        }

        public async Task<ProductResponse> GetProductById(int id)
        {

            try
            {
                // var product = await (from prod in dBContext.Products
                //                      where prod.ProductId == id
                //                      select prod).FirstOrDefaultAsync();

                // var product = await dBContext.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.ProductId == id);
                // return product;

                var product = await dBContext.Products
                .Where(x => x.ProductId == id)
                .Select(x => new ProductResponse
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Price = x.Price,
                    Description = x.Description,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category != null
                                    ? x.Category.CategoryName
                                    : string.Empty
                })
                .FirstOrDefaultAsync();
                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception();
            }
        }

        public async Task<Product> AddProduct(AddProductRequest product)
        {
            try
            {
                Product newProduct = new Product
                {
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Description = product.Description,
                    CategoryId = product.CategoryId
                };
                await dBContext.Products.AddAsync(newProduct);

                await dBContext.SaveChangesAsync();

                return newProduct;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception();
            }

        }

        public async Task<Product> UpdateProduct(Product product)
        {
            try
            {
                Product? existing =
                await dBContext.Products.FindAsync(product.ProductId);

                if (existing == null)
                    throw new Exception("Product not found");

                existing.ProductName = product.ProductName;
                existing.Price = product.Price;
                existing.CategoryId = product.CategoryId;

                await dBContext.SaveChangesAsync();

                return existing;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception();
            }
        }

        public async Task<bool> CategoryExists(int categoryId)
        {
            try
            {
                return await dBContext.Categories
                .AnyAsync(x => x.CategoryId == categoryId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception();
            }

        }
    }
}
