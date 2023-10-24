using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[ApiController]
	[Tags("Categories")]
	[Route("categories")]
	[Produces("application/json")]
	[Consumes("application/json")]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		/// <summary>
		/// Get list of categories.
		/// </summary>
		/// <response code="200">The categories was found</response>
		/// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
		/// <response code="500">A server fault occurred</response>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ResponseCache(CacheProfileName = "DefaultCacheProfile")]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetCategoryModel>>> Get()
		{
			return Ok(await _categoryService.ListAsync());
		}

		/// <summary>
		/// Find a category by ID
		/// </summary>
		/// <response code="200">The category was found</response>
		/// <response code="404">The category was not found</response>
		/// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
		/// <response code="500">A server fault occurred</response>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ResponseCache(CacheProfileName = "DefaultCacheProfile")]
		[HttpGet("{id}")]
		public async Task<ActionResult<GetCategoryModel>> Get(int id)
		{
			var category = await _categoryService.GetByIdAsync(id);
			if (category == null) {
				return NotFound();
			}
			return Ok(category);
		}

		/// <summary>
		/// Update an existing category
		/// </summary>
		/// <response code="200">The category was updated successfully</response>
		/// <response code="400">The request could not be understood by the server due to malformed syntax. The client should not repeat the request without modifications</response>
		/// <response code="404">The category was not found for specified category ID</response>
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
		public async Task<ActionResult<GetCategoryModel>> Put(int id, PostCategoryModel category)
		{
			var result = await _categoryService.UpdateAsync(id, category);
			return result == null ? NotFound() : Ok(result);
		}

		/// <summary>
		/// Create a new category
		/// </summary>
		/// <response code="201">The category was created successfully. Also includes 'location' header to newly created item</response>
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
		public async Task<ActionResult<GetCategoryModel>> Post(PostCategoryModel category)
		{
			var createdCategory = await _categoryService.CreateAsync(category);
			return CreatedAtAction(nameof(Get), routeValues: new { id = createdCategory.Id }, createdCategory);
		}

		/// <summary>
		/// Delete category with the related items
		/// </summary>
		/// <response code="200">The category was deleted successfully.</response>
		/// <response code="404">An category having specified item ID was not found</response>
		/// <response code="406">When a request is specified in an unsupported content type using the Accept header</response>
		/// <response code="500">A server fault occurred</response>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[HttpDelete("{id}")]
		public async Task<ActionResult<GetCategoryModel>> Delete(int id)
		{
			var category = await _categoryService.DeleteAsync(id);
			if (category == null) {
				return NotFound();
			}
			return Ok(category);
		}
	}
}