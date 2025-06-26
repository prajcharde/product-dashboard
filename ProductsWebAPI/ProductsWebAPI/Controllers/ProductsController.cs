
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductsDTOs.Classes;
using ProductsServices.Interfaces;

namespace ProductsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, IMapper mapper, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Gets a list of all products.
        /// </summary>
        /// <returns>A list of products.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            _logger.LogInformation("Fetching all products.");

            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products.");
                return StatusCode(500, "Internal server error");
            }
        }

        // <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productDto">The product data to create the new product.</param>
        /// <returns>A newly created product in DTO form.</returns>
        /// <response code="201">The product was successfully created.</response>
        /// <response code="400">The request data is invalid.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(CreateProductDTO productDto)
        {
            _logger.LogInformation("Creating a new product: {ProductName}", productDto.Name);

            try
            {
                var createdProduct = await _productService.CreateProductAsync(productDto);
                _logger.LogInformation("Product successfully created with ID: {ProductId}", createdProduct.Id);
                return StatusCode(201, createdProduct);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid data for product creation.");
                return BadRequest(new { message = "Invalid product data.", details = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a product.");
                return StatusCode(500, new { message = "Internal server error. Please try again later." });
            }
        }
    }
}
