using Microsoft.EntityFrameworkCore;
using ProductsRepository.DataModels;


namespace ProductsReepository.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
