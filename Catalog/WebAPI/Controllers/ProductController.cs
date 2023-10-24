using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[ApiController]
	[Tags("Items")]
	[Route("items")]
	[Produces("application/json")]
	[Consumes("application/json")]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		/// <summary>
		/// Find an item by ID
		/// </summary>
		/// <response code="200">The item was found</response>
		/// <response code="404">The item was not found</response>
		/// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
		/// <response code="500">A server fault occurred</response>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpGet("{id}", Name = nameof(GetItem))]
		[ResponseCache(CacheProfileName = "DefaultCacheProfile")]
		public async Task<ActionResult<Product>> GetItem(int id)
		{
			var category = await _productService.GetByIdAsync(id);
			if (category == null) {
				return NotFound();
			}
			return Ok(category);
		}

		/// <summary>
		/// Get a paginated list of items. If no pagination query string is specified, all elements will be returned
		/// </summary>
		/// <response code="200">The items was found</response>
		/// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
		/// <response code="500">A server fault occurred</response>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpGet(Name = nameof(GetItems))]
		[ResponseCache(CacheProfileName = "DefaultCacheProfile")]
		public async Task<ActionResult<IEnumerable<Product>>> GetItems([FromQuery] ProductQueryParams queryParams)
		{
			if (!HttpContext.Request.QueryString.HasValue) {
				return Ok(await _productService.ListAsync());
			}

			return Ok(await _productService.FindListAsync(queryParams));
		}

		/// <summary>
		/// Update an existing item
		/// </summary>
		/// <response code="200">The item was updated successfully</response>
		/// <response code="400">The request could not be understood by the server due to malformed syntax. The client should not repeat the request without modifications</response>
		/// <response code="404">The item was not found for specified item ID</response>
		/// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
		/// <response code="415">When a response is specified in an unsupported content type</response>
		/// <response code="500">A server fault occurred</response>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		[ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPut("{id}")]
		public async Task<ActionResult<Product>> Put(int id, Product product)
		{
			var result = await _productService.UpdateAsync(id, product);
			return result == null ? NotFound() : Ok(result);
		}

		/// <summary>
		/// Create a new item
		/// </summary>
		/// <response code="201">The item was created successfully. Also includes 'location' header to newly created item</response>
		/// <response code="400">The request could not be understood by the server due to malformed syntax. The client should not repeat the request without modifications</response>
		/// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
		/// <response code="415">When a response is specified in an unsupported content type</response>
		/// <response code="500">A server fault occurred</response>
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		[ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpPost]
		public async Task<ActionResult<Product>> Post(Product product)
		{
			var createdProduct = await _productService.CreateAsync(product);
			return CreatedAtAction(nameof(GetItem), routeValues: new { id = createdProduct.Id }, createdProduct);
		}

		/// <summary>
		/// Delete item
		/// </summary>
		/// <response code="200">The item was deleted successfully.</response>
		/// <response code="404">An item having specified item ID was not found</response>
		/// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
		/// <response code="500">A server fault occurred</response>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
