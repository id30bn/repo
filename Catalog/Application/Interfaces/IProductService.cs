using Application.Models;

namespace Application.Interfaces
{
	public interface IProductService
	{
		Task<IEnumerable<GetItemModel>> ListAsync();

		Task<IEnumerable<GetItemModel>> FindListAsync(ItemQueryParams queryParams);

		Task<GetItemModel> GetByIdAsync(int id);

		Task<GetItemModel> CreateAsync(PostItemModel item);

		Task<GetItemModel> UpdateAsync(int id, PostItemModel item);

		Task<GetItemModel> DeleteAsync(int id);
	}
}
