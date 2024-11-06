using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCbrAssignment.Common.ExceptionModels;
using ProductCbrAssignment.Domain.Abstractions.Products;
using ProductCbrAssignment.Domain.DTOs.Products;

namespace ProductCbrAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<ProductDetailResponse>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailResponse>> GetProduct(string id)
        {
            try
            {
                var product = await _productService.GetProductDetail(id);
                return Ok(product);
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<bool>> CreateProduct([FromBody] ProductCreateRequest request)
        {
            var created = await _productService.CreateProductAsync(request);
            if (created)
            {
                return Created(string.Empty, "Product created successfully.");
            }
            return BadRequest("Failed to create product.");
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateProduct(string id, [FromBody] ProductUpdateRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest("Product ID mismatch.");
            }

            try
            {
                var updated = await _productService.UpdateProductRequest(request);
                return updated ? Ok("Product updated successfully.") : BadRequest("Failed to update product.");
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteProduct(string id)
        {
            try
            {
                var deleted = await _productService.DeleteProduct(id);
                return deleted ? Ok("Product deleted successfully.") : BadRequest("Failed to delete product.");
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
