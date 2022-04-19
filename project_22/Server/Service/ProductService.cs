using Microsoft.EntityFrameworkCore;

namespace project_22.Server.Service
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
    }
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = new List<Product>();
            foreach (var product in await _context.Products.Include(p => p.Category).ToListAsync())
            {
                products.Add(new Product
                {
                    Id = product.Id,
                    ArticleNumber = product.ArticleNumber,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    CategoryName = product.Category.Name
                });
            }
            return products;
        }
    }
}
