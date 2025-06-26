

using ProductsRepository.DataModels;

namespace ProductsRepository.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
    }
}
