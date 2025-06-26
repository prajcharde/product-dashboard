using ProductsDTOs.Classes;

namespace ProductsServices.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO> CreateProductAsync(CreateProductDTO productDto);
    }
}
