using Microsoft.EntityFrameworkCore;

namespace project_22.Server.Services
{
    public interface IProductService
    {
        Task<ServiceResponse<IEnumerable<Product>>> GetAllAsync();
        Task<ServiceResponse<Product>> GetByIdAsync(int id);
    }
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<IEnumerable<Product>>> GetAllAsync()
        {
            var products = new List<Product>();
            var response = new ServiceResponse<IEnumerable<Product>>
            {
                Data = products
            };
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
            return response;
        }

        public async Task<ServiceResponse<Product>> GetByIdAsync(int id)
        {
            var product = await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(p => p.Id == id);
            var response = new ServiceResponse<Product>();
            if (product != null)
            {
                response.Data = new Product
                {
                    Id = product.Id,
                    ArticleNumber = product.ArticleNumber,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    CategoryName = product.Category.Name
                };           
            }
            else
            {
                response.Success = false;
                response.Message = "Tyvärr, vi har inte hittat vad du sökt.";
            }
            return response;
        }
    }
}
