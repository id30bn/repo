using Application.Models;

namespace Application.Interfaces
{
	public interface ICategoryService
	{
		Task<IEnumerable<CategoryDTO>> ListAsync();

		Task<CategoryDTO> GetByIdAsync(int id);

		Task<CategoryDTO> CreateAsync(CategoryDTO category);

		Task<CategoryDTO> UpdateAsync(int id, CategoryDTO category);

		Task<CategoryDTO> DeleteAsync(int id);
	}
}
