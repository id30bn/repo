using Application.Models;

namespace Application.Interfaces
{
	public interface ICategoryService
	{
		Task<IEnumerable<GetCategoryModel>> ListAsync();

		Task<GetCategoryModel> GetByIdAsync(int? id); // we should have it nullable as root category cannot have parentId

		Task<GetCategoryModel> CreateAsync(PostCategoryModel category);

		Task<GetCategoryModel> UpdateAsync(int id, PostCategoryModel category);

		Task<GetCategoryModel> DeleteAsync(int id);
	}
}
