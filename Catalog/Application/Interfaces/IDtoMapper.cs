using Application.Models;
using Domain.CategoryAggregate;

namespace Application.Interfaces
{
	public interface IDtoMapper
	{
		Category MapToDomainCategory(CategoryDTO category);

		CategoryDTO MapToCategoryDTO(Category category);

		Item MapToDomainProduct(Product product);

		Product MapToProductDTO(Item item);
	}
}
