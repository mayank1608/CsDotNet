using EcommWebAPI.EcomDAL;
using EcommWebAPI.Models;
using EcommWebAPI.EcomBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;

namespace EcommWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryBL categoryBLRepository;

        public CategoryController(ICategoryBL categoryBL)
        {
            categoryBLRepository = categoryBL;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var categoryList = new List<Category>();
            try
            {
                categoryList = await categoryBLRepository.GetAllCategory();
                return Ok(categoryList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
                return NotFound();
            }
        }

        [HttpGet]

        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await categoryBLRepository.GetCategoryById(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
                return NotFound();
            }
        }

        [HttpGet]

        public async Task<IActionResult> GetCategoryByName(string name)
        {
            try
            {
                var category = await categoryBLRepository.GetCategoryByName(name);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category input_category)
        {
            Category category = new Category();
            try
            {
                category = await categoryBLRepository.AddCategory(input_category);
                return Ok(category);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
                return NotFound();
            }
        }

        [HttpDelete]

        public async Task<IActionResult> RemoveCategory(int id)
        {
            try
            {
                var category = await categoryBLRepository.RemoveCategory(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
                return NotFound();
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(Category input, int id)
        {
            try
            {
                var category = await categoryBLRepository.UpdateCategory(input, id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
                return NotFound();
            }

        }
    }
}
