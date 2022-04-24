using project_22.Shared.Entity;

namespace project_22.Server.Services
{
    public interface ICategoryService
    {
        Task<CategoryEntity> GetAsync(string categoryName);
    }
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<CategoryEntity> GetAsync(string categoryName)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName.ToLower());
            if (category != null)
                return category;

            return null!;
        }
    }
}
