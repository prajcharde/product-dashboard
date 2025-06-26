
using AutoMapper;
using Moq;
using ProductsDTOs.Classes;
using ProductsRepository.DataModels;
using ProductsRepository.Interfaces;
using ProductsServices.Classes;

namespace ProductsTest.ServicesTest
{
    public class CategoriesServiceTest
    {
        private readonly Mock<ICategoryRepository> _categoryRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CategoryService _categoryService;

        public CategoriesServiceTest()
        {
            _categoryRepoMock = new Mock<ICategoryRepository>();
            _mapperMock = new Mock<IMapper>();
            _categoryService = new CategoryService(_categoryRepoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllCategoriesAsync_ReturnsMappedCategoryDTOs()
        {
            // Arrange
            var categoryEntities = new List<Category>
            {
                new Category { Id = 1, Name = "Food" },
                new Category { Id = 2, Name = "Electronics" }
            };

            var categoryDTOs = new List<CategoryDTO>
            {
                new CategoryDTO { Id = 1, Name = "Food" },
                new CategoryDTO { Id = 2, Name = "Electronics" }
            };

            _categoryRepoMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(categoryEntities);

            _mapperMock
                .Setup(m => m.Map<IEnumerable<CategoryDTO>>(categoryEntities))
                .Returns(categoryDTOs);

            // Act
            var result = await _categoryService.GetAllCategoriesAsync();

            // Assert
            Assert.NotNull(result);
            var resultList = Assert.IsAssignableFrom<IEnumerable<CategoryDTO>>(result);
            Assert.Collection(resultList,
                item => Assert.Equal("Food", item.Name),
                item => Assert.Equal("Electronics", item.Name));
        }
    }
}
