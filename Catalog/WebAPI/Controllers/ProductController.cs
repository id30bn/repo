using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> Get()
		{
			return Ok(await _productService.ListAsync());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> Get(int id)
		{
			var category = await _productService.GetByIdAsync(id);
			if (category == null) {
				return NotFound();
			}
			return Ok(category);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<Product>> Put(int id, Product product)
		{
			var result = await _productService.UpdateAsync(id, product);
			return result == null ? NotFound() : Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<Product>> Post(Product product)
		{
			var createdProduct = await _productService.CreateAsync(product);
			return CreatedAtAction(nameof(Get), new { id = createdProduct.Id }, createdProduct);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<Product>> Delete(int id)
		{
			var product = await _productService.DeleteAsync(id);
			if (product == null) {
				return NotFound();
			}
			return Ok(product);
		}
	}
}
