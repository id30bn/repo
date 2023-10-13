using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
		{
			return Ok(await _categoryService.ListAsync());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<CategoryDTO>> Get(int id)
		{
			var category = await _categoryService.GetByIdAsync(id);
			if (category == null) {
				return NotFound();
			}
			return Ok(category);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<CategoryDTO>> Put(int id, CategoryDTO category)
		{
			var result = await _categoryService.UpdateAsync(id, category);
			return result == null ? NotFound() : Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<CategoryDTO>> Post(CategoryDTO category)
		{
			var createdCategory = await _categoryService.CreateAsync(category);
			return CreatedAtAction(nameof(Get), new { id = createdCategory.Id }, createdCategory);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<CategoryDTO>> Delete(int id)
		{
			var category = await _categoryService.DeleteAsync(id);
			if (category == null) {
				return NotFound();
			}
			return Ok(category);
		}
	}
}