using AutoMapper;
using Domain.CategoryAggregate;

namespace Application.Models.Mappers
{
	public class CategoryMapperProfile : Profile
	{
		public CategoryMapperProfile()
		{
			CreateMap<Category, GetCategoryModel>()
				.ForMember(d => d.ImageUrl, opt => opt.Condition(s => s.Image?.Url != null));

			CreateMap<PostCategoryModel, Category>()
				.ConstructUsing(x => new Category(x.Name, x.ParentId, x.ImageUrl));
		}
	}
}
