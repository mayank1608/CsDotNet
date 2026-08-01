using Microsoft.AspNetCore.Mvc;
using ProductCatalog.BL.Interfaces;

namespace ProductCatalog.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(
        IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var result =
            await _service.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(
        int id)
    {
        var result =
            await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(
        Product product)
    {
        var result =
            await _service.AddAsync(product);

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(
        Product product)
    {
        var result =
            await _service.UpdateAsync(product);

        return Ok(result);
    }
}