using Application.Interfaces;
using Application.Models;
using GraphQL.Types;

namespace GraphQLServer.Schema
{
	public class ItemType : ObjectGraphType<GetItemModel>
	{
		public ItemType() {
			Field(x => x.Id);
			Field(x => x.Name);
			Field(x => x.ImageUrl, nullable: true);
			Field(x => x.Description);
			Field(x => x.Price);
			Field(x => x.Amount);
			Field(x => x.CategoryId);
			Field<CategoryType>("category")
				.ResolveAsync(async context => await context.RequestServices
					.GetRequiredService<ICategoryService>().GetByIdAsync(context.Source.CategoryId));
		}
	}
}
