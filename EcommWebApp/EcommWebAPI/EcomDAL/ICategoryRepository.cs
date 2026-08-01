using EcommWebAPI.Models;

namespace EcommWebAPI.EcomDAL
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAllCategory();
        public Task<Category> AddCategory(Category category);

        public Task<Category> GetCategoryById(int id);
        public Task<Category> GetCategoryByName(string name);

        public Task<Category> RemoveCategory(int id);

        public Task<Category> UpdateCategory(Category category, int id);

    }
}
