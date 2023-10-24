using Application.Models;
using Domain.CategoryAggregate;

namespace Application.Interfaces
{
	[Obsolete("Use AutoMapper profiles instead")]
	public interface IDtoMapper
	{
		Category MapToDomainCategory(GetCategoryModel category);

		GetCategoryModel MapToCategoryDTO(Category category);

		Item MapToDomainProduct(GetItemModel product);

		GetItemModel MapToProductDTO(Item item);
	}
}
