using EcommWebAPI.EcomBL;
using EcommWebAPI.EcomDAL;
using EcommWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EcommWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]

    public class ProductController : ControllerBase
    {
        private readonly IProductBL _productBL;

        public ProductController(IProductBL productBL)
        {
            _productBL = productBL;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var productList = new List<Product>();
            try
            {
                productList = await _productBL.GetAllProducts();
                return Ok(productList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
                return NotFound();
            }
        }

        [HttpGet]

        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productBL.GetProductById(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
                return NotFound();
            }
        }

        [HttpGet]

        public async Task<IActionResult> GetProductByName(string Productname)
        {
            try
            {
                var product = await _productBL.GetProductByName(Productname);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
                return NotFound();
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddProductDetails(Product input_product)
        {
            Product product = new Product();
            try
            {
                product = await _productBL.AddProductDetails(input_product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
                return NotFound();
            }
        }

        [HttpDelete]

        public async Task<IActionResult> RemoveProduct(int id)
        {
            try
            {
                var product = await _productBL.RemoveProduct(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}"); 
                return NotFound();
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product input_product, int id)
        {
            try
            {
                var product = await _productBL.UpdateProduct(input_product, id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
                return NotFound();
            }

        }

        [HttpPatch]
        public async Task<IActionResult> UpdateProductPrice(double price, int productId)
        {
            try
            {
                var product = await _productBL.UpdateProductPrice(price, productId); // Ensure arguments are in correct order
                if (product == null) // Handle null case correctly
                {
                    return NotFound($"Product with ID {productId} does not exist.");
                }
                return Ok(product); // Return product when successfully updated
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
                return StatusCode(500, "An error occurred while updating the product price.");
            }
        }

    }
}
