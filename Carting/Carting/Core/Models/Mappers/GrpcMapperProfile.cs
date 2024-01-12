using AutoMapper;
using Carting.API.gRPC;
using Carting.Core.Models.Cart;

namespace Carting.Core.Models.Mappers
{
	public class GrpcMapperProfile : Profile
	{
		public GrpcMapperProfile()
		{
			//CreateMap(typeof(IEnumerable<>), typeof(RepeatedField<>)).ConvertUsing(typeof(EnumerableToRepeatedFieldTypeConverter<,>));
			//CreateMap(typeof(RepeatedField<>), typeof(List<>)).ConvertUsing(typeof(EnumerableToRepeatedFieldTypeConverter<,>));
			
			CreateMap<Item, ItemMessage>(MemberList.Source) //source for simplicity
				.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // Protobuf has no concept of null; with proto3 treating en empty string
			CreateMap<Image, ImageMessage>(MemberList.Source)
				.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

			CreateMap<ItemMessage, Item>(MemberList.Destination);
			CreateMap<ImageMessage, Image>(MemberList.Destination);
		}
	}
}
