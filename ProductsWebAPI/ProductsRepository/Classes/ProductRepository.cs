using Microsoft.EntityFrameworkCore;
using ProductsReepository.Data;
using ProductsRepository.DataModels;
using ProductsRepository.Interfaces;

namespace ProductsRepository.Classes
{
    public class ProductRepository: IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                                 .Include(p => p.Category)
                                 .ToListAsync();
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
