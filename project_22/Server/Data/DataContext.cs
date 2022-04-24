using Microsoft.EntityFrameworkCore;
using project_22.Shared.Entity;

namespace project_22.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>().HasKey(c => new { c.UserId, c.ProductId});
            modelBuilder.Entity<OrderItem>().HasKey(o => new { o.OrderId, o.ProductId });

            modelBuilder.Entity<ProductEntity>().HasData(
                new ProductEntity
                {
                    Id = 1,
                    ArticleNumber = "VBA420AE",
                    ProductName = "Nikon D750",
                    Description = "Nikon D750 är en digital systemkamera som ger dig frihet att lyckas fånga alla motiv.",
                    ImageUrl = "https://images.unsplash.com/photo-1621520291095-aa6c7137f048?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80",
                    Price = 11900,
                    CategoryId = 1
                },
                new ProductEntity
                {
                    Id = 2,
                    ArticleNumber = "20945",
                    ProductName = "Polaroid Now",
                    Description = "Polaroid Now är en klassisk direktfilmskamera som plåtar och framkallar polaroidbilder med riktig retrokänsla. ",
                    ImageUrl = "https://www.kjell.com/globalassets/productimages/839191_20945.tif?ref=D5872CA259&format=jpg&w=960&h=960&mode=pad",
                    Price = 2100,
                    CategoryId = 1,
                },
                new ProductEntity
                {
                    Id = 3,
                    ArticleNumber = "VINTA145",
                    ProductName = "VINTA TYPE-II",
                    Description = "This bag is perfect for a lifestyle of adventure, trekking to and from the studio, the office or the mountains.",
                    ImageUrl = "https://images.unsplash.com/photo-1547949003-9792a18a2601?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80",
                    Price = 1800,
                    CategoryId = 2,
                },
                new ProductEntity
                {
                    Id = 4,
                    ArticleNumber = "1000XM4",
                    ProductName = "Sony WH-1000XM4",
                    Description = "WH-1000XM4-hörlurarna kombinerar elegant design och komfort på hög nivå. Supermjuka, tryckavlastande kuddar i uretanskum ger jämnt tryck och ökad kontakt med öronen för god passform. Den lägre vikten gör att du knappt kommer känna att du har dem på dig.",
                    ImageUrl = "https://images.unsplash.com/photo-1546435770-a3e426bf472b?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1165&q=80",
                    Price = 1699,
                    CategoryId = 3,
                },
                new ProductEntity
                {
                    Id = 5,
                    ArticleNumber = "JBLGO3NHJ",
                    ProductName = "JBL GO 3",
                    Description = "JBL GO 3 är den perfekta högtalaren att ta med dig på språng! Med färgladd tyg och uttrycksfulla detaljer som är inspi- rerade av street style, är detta tillbehör en nödvändighet på din nästa utflykt. GO 3 är vatten- och dammtät enligt IP67 och har en integrerade ögla som gör att du kan bära med den vart som helst.",
                    ImageUrl = "https://images.unsplash.com/photo-1608223652565-63abae6cf733?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80",
                    Price = 365,
                    CategoryId = 4,
                }
            );

            modelBuilder.Entity<CategoryEntity>().HasData(
                new CategoryEntity
                {
                    Id = 1,
                    Name = "Kamera"
                },
                new CategoryEntity
                {
                    Id = 2,
                    Name = "Kamera Väska"
                },
                new CategoryEntity 
                { 
                    Id = 3, 
                    Name = "Hörlurar"
                },
                new CategoryEntity
                {
                    Id = 4,
                    Name = "Högtalare"
                }
            );

        }

        public virtual DbSet<ProductEntity> Products { get; set; } = null!;
        public virtual DbSet<CategoryEntity> Categories { get; set; } = null!;
        public virtual DbSet<UserEntity> Users { get; set; } = null!;
        public virtual DbSet<CartItem> CartItems { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;

    }
}
