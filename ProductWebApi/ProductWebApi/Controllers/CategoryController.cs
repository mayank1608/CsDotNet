using Microsoft.AspNetCore.Mvc;
using ProductWebApi.BLRepo;
using ProductWebApi.Models;

namespace ProductWebApi.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryBL _service;

    public CategoriesController(
        ICategoryBL service)
    {
        _service = service;
    }

    [HttpGet("all-categories")]
    public async Task<IActionResult> GetAllCategories()
    {
        var result =
            await _service.GetAllCategories();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(
        int id)
    {
        var result =
            await _service.GetCategoryById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("add-category")]
    public async Task<IActionResult> AddCategory(
        Category category)
    {
        var result =
            await _service.AddCategory(category);

        return Ok(result);
    }

    [HttpPut("update-category")]
    public async Task<IActionResult> UpdateCategory(
        Category category)
    {
        var result =
            await _service.UpdateCategory(category);

        return Ok(result);
    }
}