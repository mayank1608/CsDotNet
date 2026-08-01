using Microsoft.AspNetCore.Mvc;
using ProductWebApi.BLRepo;
using ProductWebApi.Models;

namespace ProductWebApi.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class ProductsController : ControllerBase
{
    private readonly IProductBL _service;

    public ProductsController(
        IProductBL service)
    {
        _service = service;
    }

    [HttpGet("all-products")]
    public async Task<IActionResult> GetProducts()
    {
        var result =
            await _service.GetAllProductDetails();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(
        int id)
    {
        var result =
            await _service.GetProductById(id);

        // if (result == null)
        //     return NotFound();

        if (!result.Success)
        {
            return NotFound(new
            {
                Message = result.Message
            });
        }

        return Ok(result);
    }

    [HttpPost("add-product")]
    public async Task<IActionResult> AddProduct(
        AddProductRequest product)
    {
        try
        {
            var result = await _service.AddProduct(product);
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(new
            {
                Message = ex.Message
            });
        }

    }

    [HttpPut("update-product")]
    public async Task<IActionResult> UpdateProduct(
        Product product)
    {
        var result =
            await _service.UpdateProduct(product);

        return Ok(result);
    }
}