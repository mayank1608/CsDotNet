using EcommWebAPI.EcomDAL;
using EcommWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommWebAPI.EcomBL
{
    public class CategoryBL: ICategoryBL
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryBL(ICategoryRepository categoryDAL)
        {
            categoryRepository = categoryDAL;
        }

        public async Task<List<Category>> GetAllCategory()
        {

            var categoryList = new List<Category>();
            try
            {
                categoryList = await categoryRepository.GetAllCategory();
                return categoryList;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Business Layer: {ex.Message}");
            }
        }

        public async Task<Category> AddCategory(Category input_category)
        {
            Category category = new Category();
            try
            {
                category = await categoryRepository.AddCategory(input_category);
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Business Layer: {ex.Message}");
            }
        }

        public async Task<Category> GetCategoryById(int id)
        {
            Category category = new Category();
            try
            {
                category = await categoryRepository.GetCategoryById(id);
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Business Layer: {ex.Message}");
            }

        }

        public async Task<Category> GetCategoryByName(string name)
        {
            Category category = new Category();
            try
            {
                category = await categoryRepository.GetCategoryByName(name);
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Business Layer: {ex.Message}");
            }

        }

        public async Task<Category> RemoveCategory(int id)
        {

            try
            {
                var category = await categoryRepository.RemoveCategory(id);
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Business Layer: {ex.Message}");
            }
        }

        public async Task<Category> UpdateCategory(Category input, int id)
        {
            try
            {
                var category = await categoryRepository.UpdateCategory(input, id);
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Business Layer: {ex.Message}");
            }
        }
    }
}
