using ProductWebApi.Models;
namespace ProductWebApi.DALRepo
{
    public interface ICategoryDALRepo
    {
        public Task<List<Category>> GetAllCategories();
        public Task<Category> GetCategoryById(int id);
        public Task<Category> AddCategory(Category category);
        public Task<Category> UpdateCategory(Category category);
    }
}
