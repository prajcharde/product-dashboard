

using ProductsRepository.DataModels;

namespace ProductsRepository.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> CreateAsync(Product product);
    }
}
