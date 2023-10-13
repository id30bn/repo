using Application.Interfaces;
using Application.Models;
using Domain.CategoryAggregate;

namespace Application.Services
{
	public class DtoMapper : IDtoMapper
	{
		public Category MapToDomainCategory(CategoryDTO category)
		{
			if (category == null) {
				return null;
			}

			return new Category(category.Name, category.ParentId, category.ImageUrl);
		}

		public CategoryDTO MapToCategoryDTO(Category category)
		{
			if (category == null) {
				return null;
			}

			return new CategoryDTO {
				Id = category.Id,
				Name = category.Name,
				ImageUrl = category.Image?.Url,
				ParentId = category.ParentId,
				Parent = MapToCategoryDTO(category.Parent)
			};
		}

		public Item MapToDomainProduct(Product product)
		{
			if (product == null) {
				return null;
			}

			return new Item(product.Name,
				product.CategoryId,
				product.Price,
				product.Amount,
				product.ImageUrl,
				product.Description
			);
		}

		public Product MapToProductDTO(Item item)
		{
			if (item == null) {
				return null;
			}

			return new Product {
				Id = item.Id,
				Name = item.Name,
				ImageUrl = item.Image?.Url,
				CategoryId = item.CategoryId,
				Category = MapToCategoryDTO(item.Category),
				Description = item.Description?.Text,
				IsHtmlDescription = item.Description?.IsHtml,
				Amount = item.Amount,
				Price = item.Price
			};
		}
	}
}
