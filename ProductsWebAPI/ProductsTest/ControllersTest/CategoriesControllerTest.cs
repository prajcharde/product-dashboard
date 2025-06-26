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
    public class CategoriesControllerTest
    {
        private readonly Mock<ICategoryService> _categoriesServiceMock;
        private readonly Mock<ILogger<CategoriesController>> _loggerMock;
        private readonly Mock<IMapper> _mapper;
        private readonly CategoriesController _controller;

        public CategoriesControllerTest()
        {
            _categoriesServiceMock = new Mock<ICategoryService>();
            _mapper = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<CategoriesController>>();
            _controller = new CategoriesController(_categoriesServiceMock.Object, _mapper.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetProducts_ReturnOkResult()
        {
            // Arrange
            List<CategoryDTO> expectedProductList = new List<CategoryDTO>();
            CategoryDTO CategoryDTO1 = new CategoryDTO { Id = 1, Name = "TestCategory1" };
            expectedProductList.Add(CategoryDTO1);
            CategoryDTO CategoryDTO2 = new CategoryDTO { Id = 1, Name = "TestCategory2" };
            expectedProductList.Add(CategoryDTO2);
            _categoriesServiceMock.Setup(service => service.GetAllCategoriesAsync()).ReturnsAsync(expectedProductList);

            // Act
            var result = await _controller.GetAllCategoriesAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualForklifts = Assert.IsType<List<CategoryDTO>>(okResult.Value);
            Assert.Equal(expectedProductList.Count, actualForklifts.Count);
        }

        [Fact]
        public async Task GetProducts_ReturnInternalServerError()
        {
            // Arrange
            _categoriesServiceMock.Setup(service => service.GetAllCategoriesAsync()).ThrowsAsync(new Exception("Database Failure"));

            // Act
            var result = await _controller.GetAllCategoriesAsync();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
            Assert.Equal("Internal server error", statusCodeResult.Value);
        }
    }
}
