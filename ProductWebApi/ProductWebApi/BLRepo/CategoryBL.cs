using ProductWebApi.Models;
using ProductWebApi.DALRepo;

namespace ProductWebApi.BLRepo
{
    public class CategoryBL: ICategoryBL
    {
        public readonly ICategoryDALRepo _categoryDAL;
        public CategoryBL(ICategoryDALRepo categoryDAL)
        {
            _categoryDAL = categoryDAL;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            var categories = new List<Category>();

            try
            {
                categories = await _categoryDAL.GetAllCategories();
                return categories;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception();
            }
        }

        public async Task<Category> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryDAL.GetCategoryById(id);
                return category;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception();
            }
        }

        public async Task<Category> AddCategory(Category category)
        {
            try
            {
                return await _categoryDAL.AddCategory(category);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception();
            }

        }

        public async Task<Category> UpdateCategory(Category category)
        {
            try
            {
                return await _categoryDAL.UpdateCategory(category);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception();
            }

        }
    }
}
