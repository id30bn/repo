using Application.Interfaces;
using Application.Models;
using Domain.CategoryAggregate;

namespace Application.Services
{
	[Obsolete("Use AutoMapper profiles instead")]
	public class DtoMapper : IDtoMapper
	{
		public Category MapToDomainCategory(GetCategoryModel category)
		{
			if (category == null) {
				return null;
			}

			return new Category(category.Name, category.ParentId, category.ImageUrl);
		}

		public GetCategoryModel MapToCategoryDTO(Category category)
		{
			if (category == null) {
				return null;
			}

			return new GetCategoryModel {
				Id = category.Id,
				Name = category.Name,
				ImageUrl = category.Image?.Url,
				ParentId = category.ParentId,
				Parent = MapToCategoryDTO(category.Parent)
			};
		}

		public Item MapToDomainProduct(GetItemModel product)
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

		public GetItemModel MapToProductDTO(Item item)
		{
			if (item == null) {
				return null;
			}

			return new GetItemModel {
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
