using EcommWebAPI.EcomDAL;
using EcommWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EcommWebAPI.DALRepositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EcommDbContext _context;

        public List<Category> Category = new();

        public CategoryRepository(EcommDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAllCategory()
        {
            var categories = new List<Category>();
            try
            {
                categories = await _context.Categories.Include(c=>c.Products).ToListAsync();
                if(categories == null)
                {
                    throw new Exception($"Categories not found.");
                }
                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching category: {ex.Message}");
            }
        }

        public async Task<Category> AddCategory(Category input_category)
        {
            try
            {
                // Reset the ID to ensure it's auto-generated
                var category = new Category
                {
                    CategoryName = input_category.CategoryName
                };
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();

                return category;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching category: {ex.Message}");
            }
        }

        public async Task<Category> GetCategoryById(int id)
        {
            try
            {
                var category = await (from p in _context.Categories
                                      where p.CategoryId == id
                                      select p).FirstOrDefaultAsync();
                if (category == null)
                {
                    throw new Exception($"Category with ID {id} not found.");
                }
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching category: {ex.Message}");
            }
        }
        public async Task<Category> GetCategoryByName(string name)
        {
            try
            {
                var category = await (from p in _context.Categories
                                      where p.CategoryName == name
                                      select p).FirstOrDefaultAsync();

                if (category == null)
                {
                    throw new Exception($"Category with Name {name} not found.");
                }

                return category;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching category: {ex.Message}");
            }
        }
        public async Task<Category> RemoveCategory(int id)
        {
            try
            {
                var category = await (from p in _context.Categories
                                      where p.CategoryId == id
                                      select p).FirstOrDefaultAsync();
                if (category == null)
                {
                    throw new Exception($"Category with ID {id} not found.");
                }
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return category;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching category: {ex.Message}");
            }
        }

        public async Task<Category> UpdateCategory(Category category, int id)
        {
            try
            {
                // Fetch the existing category from the database
                var existingCategory = await _context.Categories
                                                     .FirstOrDefaultAsync(p => p.CategoryId == id);
                if (existingCategory == null)
                {
                    throw new Exception($"Category with ID {id} not found.");
                }
                // Update the category name
                existingCategory.CategoryName = category.CategoryName;
                // Update the category in the database
                _context.Categories.Update(existingCategory);
                await _context.SaveChangesAsync();

                return existingCategory;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching category: {ex.Message}");
            }
        }

    }
}
