using Microsoft.EntityFrameworkCore;
using project_22.Shared.Entity;
using project_22.Shared.Form;

namespace project_22.Server.Services
{
    public interface IProductService
    {
        Task<ServiceResponse<IEnumerable<Product>>> GetAllAsync();
        Task<ServiceResponse<Product>> GetByIdAsync(int id);
        Task<ServiceResponse<Product>> CreateAsync(AddProductForm form);
    }
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Product>> CreateAsync(AddProductForm form)
        {
            if (!await _context.Products.AnyAsync(p => p.ArticleNumber == form.ArticleNumber))
            {
                var productEntity = new ProductEntity
                {
                    ArticleNumber = form.ArticleNumber,
                    ProductName = form.ProductName,
                    Description = form.Description,
                    ImageUrl = form.ImageUrl,
                    Price = form.Price
                };

                // Get category, use cartegory service in future
                var categoryEntity = await _context.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == form.CategoryName.ToLower());
                if (categoryEntity == null)
                {
                    productEntity.Category = new CategoryEntity { Name = form.CategoryName };
                }
                else
                {
                    productEntity.CategoryId = categoryEntity.Id;
                }

                _context.Products.Add(productEntity);
                await _context.SaveChangesAsync();

                return new ServiceResponse<Product>
                {
                    Data = new Product
                    {
                        Id = productEntity.Id,
                        ArticleNumber = productEntity.ArticleNumber,
                        ProductName = productEntity.ProductName,
                        ImageUrl = productEntity.ImageUrl,
                        Description = productEntity.Description,
                        Price = productEntity.Price,
                        CategoryName = productEntity.Category.Name
                    },
                    Message = "Produkten har skapats."
                };
            }
            return new ServiceResponse<Product> { Data = null!, Success = false, Message = "Kunde inte skapa produkten." };
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
