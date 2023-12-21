using Application.Interfaces;
using Application.Models;
using GraphQL;
using GraphQL.Types;

namespace GraphQLServer.Schema
{
	public class RootQuery : ObjectGraphType
	{
		public RootQuery() { // inject IServiceProvider if neccessary
			Name = "Queries";

			Field<ListGraphType<CategoryType>>(name: "categories")
				//.Resolve(context => {
				//	using (var scope = provider.CreateScope())
				//		return scope.ServiceProvider.GetRequiredService<ICategoryService>().ListAsync();
				//	});
				.ResolveAsync(async context => await context.RequestServices.GetRequiredService<ICategoryService>().ListAsync());

			Field<CategoryType>("category")
				.Arguments(new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }))
				.ResolveAsync(async context => await context.RequestServices
						.GetRequiredService<ICategoryService>()
						.GetByIdAsync(context.GetArgument<int>("id")));

			Field<ListGraphType<ItemType>>("items")
				.ResolveAsync(async context => await context.RequestServices.GetRequiredService<IProductService>().ListAsync());

			Field<ListGraphType<ItemType>>("items_pagination")
				.Arguments(new QueryArguments(
					new QueryArgument<IdGraphType> { Name = "categoryId" },
					new QueryArgument<IdGraphType> { Name = "page" },
					new QueryArgument<IdGraphType> { Name = "limit" }))
				.ResolveAsync(async context => {
					var queryParams = new ItemQueryParams {
						CategoryId = context.GetArgument<int>("categoryId"),
						Page = context.GetArgument<int>("page"),
						Limit = context.GetArgument<int>("limit")
					};
					return await context.RequestServices.GetRequiredService<IProductService>().FindListAsync(queryParams);
				});
		}
	}
}
