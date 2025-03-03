using Crud_Rest_API_Using_DotNet.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Crud_Rest_API_Using_DotNet.Model;
using Microsoft.EntityFrameworkCore;

namespace Crud_Rest_API_Using_DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Invalid Product Data");
            }
            _appDbContext.Products.Add(product);
             await _appDbContext.SaveChangesAsync();
            return Ok(product);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _appDbContext.Products.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _appDbContext.Products.FindAsync(id);

            if (product == null)
                return NotFound("Product not found");

            return product;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            var product = await _appDbContext.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _appDbContext.Products.Remove(product);
            await _appDbContext.SaveChangesAsync();

            return Ok(new { message = "Product deleted successfully!" });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var product = await _appDbContext.Products.FindAsync(id);
            if (product == null) return NotFound("Product not found.");

            product.Name=updatedProduct.Name;
            product.Price=updatedProduct.Price;
            await _appDbContext.SaveChangesAsync();

            return Ok(new { message = "Product updated successfully!", product });
        }
    }
}
