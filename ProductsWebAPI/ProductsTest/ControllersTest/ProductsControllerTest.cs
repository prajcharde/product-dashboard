using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProductsDTOs.Classes;
using ProductsServices.Interfaces;
using ProductsWebAPI.Controllers;

namespace ProductsTest.ControllersTest
{
    public class ProductsControllerTest
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<ILogger<ProductsController>> _loggerMock;
        private readonly Mock<IMapper> _mapper;
        private readonly ProductsController _controller;

        public ProductsControllerTest()
        {
            _productServiceMock = new Mock<IProductService>();
            _mapper = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<ProductsController>>();
            _controller = new ProductsController(_productServiceMock.Object,_mapper.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetProducts_ReturnOkResult()
        {
            // Arrange
            List<ProductDTO> expectedProductList = new List<ProductDTO>();
            ProductDTO productDTO1 = new ProductDTO{ Id = 1, Name = "TestProduct1" };
            expectedProductList.Add(productDTO1);
            ProductDTO productDTO2 = new ProductDTO { Id = 1, Name = "TestProduct2" };
            expectedProductList.Add(productDTO2);
            _productServiceMock.Setup(service => service.GetAllProductsAsync()).ReturnsAsync(expectedProductList);

            // Act
            var result = await _controller.GetAllProductsAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualForklifts = Assert.IsType<List<ProductDTO>>(okResult.Value);
            Assert.Equal(expectedProductList.Count, actualForklifts.Count);
        }

        [Fact]
        public async Task GetProducts_ReturnInternalServerError()
        {
            // Arrange
            _productServiceMock.Setup(service => service.GetAllProductsAsync()).ThrowsAsync(new Exception("Database Failure"));

            // Act
            var result = await _controller.GetAllProductsAsync();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
            Assert.Equal("Internal server error", statusCodeResult.Value);
        }

        [Fact]
        public async Task CreateProduct_Returns201Status()
        {
            // Arrange
            var inputDto = new CreateProductDTO { Name = "TestProduct" };
            var expectedProduct = new ProductDTO { Id = 1, Name = "TestProduct" };

            _productServiceMock
                .Setup(s => s.CreateProductAsync(inputDto))
                .ReturnsAsync(expectedProduct);

            // Act
            var result = await _controller.CreateProductAsync(inputDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, objectResult.StatusCode);
            Assert.Equal(expectedProduct, objectResult.Value);
        }

        [Fact]
        public async Task CreateProductAsync_Returns400_WhenArgumentExceptionThrown()
        {
            // Arrange
            var inputDto = new CreateProductDTO { Name = "InvalidProduct" };

            _productServiceMock
                .Setup(s => s.CreateProductAsync(inputDto))
                .ThrowsAsync(new ArgumentException("Name is required."));

            // Act
            var result = await _controller.CreateProductAsync(inputDto);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public async Task CreateProductAsync_Returns500_WhenUnhandledExceptionThrown()
        {
            // Arrange
            var inputDto = new CreateProductDTO { Name = "TestProduct" };

            _productServiceMock
                .Setup(s => s.CreateProductAsync(inputDto))
                .ThrowsAsync(new Exception("Database failure"));

            // Act
            var result = await _controller.CreateProductAsync(inputDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}
