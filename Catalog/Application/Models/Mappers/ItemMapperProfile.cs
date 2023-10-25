using AutoMapper;
using Domain.CategoryAggregate;

namespace Application.Models.Mappers
{
	public class ItemMapperProfile : Profile
	{
		public ItemMapperProfile()
		{
			CreateMap<Item, GetItemModel>()
				.ForMember(d => d.ImageUrl, opt => opt.Condition(s => s.Image?.Url != null))
				.ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description != null ? s.Description.Text : null))
				.ForMember(d => d.IsHtmlDescription, opt => opt.MapFrom(s => s.Description != null ? s.Description.IsHtml : (bool?)null));

			CreateMap<PostItemModel, Item>()
				.ConstructUsing(x => new Item(x.Name, x.CategoryId, x.Price, x.Amount, x.ImageUrl, x.Description));
		}
	}
}
