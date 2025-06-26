using AutoMapper;
using ProductsDTOs.Classes;
using ProductsRepository.DataModels;
using ProductsRepository.Interfaces;
using ProductsServices.Interfaces;

namespace ProductsServices.Classes
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> CreateProductAsync(CreateProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            product.DateAdded = DateTime.UtcNow;
            var saved = await _productRepository.CreateAsync(product);
            return _mapper.Map<ProductDTO>(saved);
        }
    }
}
