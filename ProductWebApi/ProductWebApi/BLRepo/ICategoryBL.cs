using ProductWebApi.Models;
namespace ProductWebApi.BLRepo
{
    public interface ICategoryBL
    {
        public Task<List<Category>> GetAllCategories();
        public Task<Category> GetCategoryById(int id);
        public Task<Category> AddCategory(Category category);
        public Task<Category> UpdateCategory(Category category);
    }
}
