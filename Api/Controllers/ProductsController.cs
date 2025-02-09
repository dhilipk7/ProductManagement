using Microsoft.AspNetCore.Mvc;
using ProductManagement.Api.Models;
using ProductManagement.Domain.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductManagement.Api.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(decimal price)
        {
            try
            {
                List<ProductDto> products = await _productService.GetAllProducts(price);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);

            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                ProductDto product = await _productService.GetProductById(id);
                if (product == null)
                {
                    return NotFound(new { message = $"Product with ID {id} not found." });
                }
                
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProductDto product = await _productService.AddProduct(productDto);
                    return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                if (ex.Message.StartsWith("Product name"))
                {
                    return StatusCode(400, ModelState);
                }
                return StatusCode(500, ModelState);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updatedProduct = await _productService.UpdateProduct(id, productDto);
                    if (updatedProduct == null)
                    {
                        return NotFound(new { message = $"Product with ID {id} not found." });
                    }

                    return Ok(updatedProduct);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }


            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                if (ex.Message.StartsWith("Product name"))
                {
                    return StatusCode(400, ModelState);
                }
                return StatusCode(500, ModelState);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                bool success = await _productService.DeleteProduct(id);
                if (!success)
                {
                    return NotFound(new { message = $"Product with ID {id} not found." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);

            }
        }
    }
}
