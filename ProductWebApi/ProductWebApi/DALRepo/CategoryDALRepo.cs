using ProductWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductWebApi.DALRepo
{
    public class CategoryDALRepo : ICategoryDALRepo
    {
        public readonly ECommDBContext dBContext;
        public CategoryDALRepo(ECommDBContext context)
        {
            // dBContext = new ECommDBContext();
            dBContext = context;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            var categories = new List<Category>();
            try
            {
                categories = await dBContext.Categories
                            .Include(c => c.Products)
                            .ToListAsync();
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
                var category = await dBContext.Categories
                               .Include(c => c.Products)
                               .FirstOrDefaultAsync(x => x.CategoryId == id);
                return category;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception();
            }
        }

        public async Task<Category> AddCategory(Category Category)
        {
            try
            {
                await dBContext.Categories.AddAsync(Category);

                await dBContext.SaveChangesAsync();

                return Category;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception();
            }

        }

        public async Task<Category> UpdateCategory(Category Category)
        {
            try
            {
                Category? existing =
                await dBContext.Categories.FindAsync(Category.CategoryId);

                if (existing == null)
                    throw new Exception("Category not found");

                existing.CategoryName = Category.CategoryName;
                existing.Products = Category.Products;

                await dBContext.SaveChangesAsync();

                return existing;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception();
            }
        }
    }
}
