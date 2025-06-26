
using AutoMapper;
using Moq;
using ProductsDTOs.Classes;
using ProductsRepository.DataModels;
using ProductsRepository.Interfaces;
using ProductsServices.Classes;

namespace ProductsTest.ServicesTest
{
    public class ProductsServiceTest
    {
        private readonly Mock<IProductRepository> _productRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProductService _productService;

        public ProductsServiceTest()
        {
            _productRepoMock = new Mock<IProductRepository>();
            _mapperMock = new Mock<IMapper>();
            _productService = new ProductService(_productRepoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync_ReturnsMappedProductDTOs()
        {
            // Arrange
            var productEntities = new List<Product>
            {
            new Product { Id = 1, Name = "Test Product 1" },
            new Product { Id = 2, Name = "Test Product 2" }
            };

            var productDTOs = new List<ProductDTO>
            {
            new ProductDTO { Id = 1, Name = "Test Product 1" },
            new ProductDTO { Id = 2, Name = "Test Product 2" }
            };

            _productRepoMock.Setup(repo => repo.GetAllAsync())
                            .ReturnsAsync(productEntities);

            _mapperMock.Setup(m => m.Map<IEnumerable<ProductDTO>>(productEntities))
                       .Returns(productDTOs);

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, ((List<ProductDTO>)result).Count);
            Assert.Collection(result,
                item => Assert.Equal("Test Product 1", item.Name),
                item => Assert.Equal("Test Product 2", item.Name));
        }

        [Fact]
        public async Task CreateProductAsync_ValidData_ReturnsMappedProductDTO()
        {
            // Arrange
            var createDto = new CreateProductDTO { Name = "New Product" };
            var product = new Product { Id = 1, Name = "New Product", DateAdded = DateTime.MinValue };
            var savedProduct = new Product { Id = 1, Name = "New Product", DateAdded = DateTime.UtcNow };
            var expectedDto = new ProductDTO { Id = 1, Name = "New Product", DateAdded = savedProduct.DateAdded };

            _mapperMock.Setup(m => m.Map<Product>(createDto)).Returns(product);
            _productRepoMock.Setup(repo => repo.CreateAsync(It.IsAny<Product>()))
                            .ReturnsAsync(savedProduct);
            _mapperMock.Setup(m => m.Map<ProductDTO>(savedProduct)).Returns(expectedDto);

            // Act
            var result = await _productService.CreateProductAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedDto.Id, result.Id);
            Assert.Equal(expectedDto.Name, result.Name);
        }
    }
}
