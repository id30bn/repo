using Application.Interfaces;
using Application.Models;
using GraphQL.Types;

namespace GraphQLServer.Schema
{
	public class CategoryType : ObjectGraphType<GetCategoryModel>
	{
		public CategoryType(IServiceProvider provider) {

			Field(x => x.Id);
			Field(x => x.Name);
			Field(x => x.ImageUrl, nullable: true); // nullable important
			Field(x => x.ParentId, nullable: true);
			Field<CategoryType>("parent")
				.ResolveAsync(async context => await context.RequestServices
					.GetRequiredService<ICategoryService>().GetByIdAsync(context.Source.ParentId)); // if 0 it will throw notfound exception
		}
	}
}
