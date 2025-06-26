using Microsoft.EntityFrameworkCore;
using ProductsReepository.Data;
using ProductsRepository.DataModels;
using ProductsRepository.Interfaces;

namespace ProductsRepository.Classes
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly ProductDbContext _context;

        public CategoryRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
