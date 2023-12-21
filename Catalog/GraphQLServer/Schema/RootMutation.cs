using Application.Interfaces;
using Application.Models;
using GraphQL;
using GraphQL.Types;

namespace GraphQLServer.Schema
{
	public class RootMutation : ObjectGraphType<object>
	{
		public RootMutation() {
			Name = "Mutations";

			Field<CategoryType>("createCategory")
				.Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<CategoryInputType>> { Name = "category" }))
				.ResolveAsync(async ctx => {
					// if neccessary create additional class (instead of PostCategoryModel) and map it to PostCategoryModel
					var inputModel = ctx.GetArgument<PostCategoryModel>("category");
					return await ctx.RequestServices.GetRequiredService<ICategoryService>().CreateAsync(inputModel);
				});
			#region example
			//mutation Mutation($category: CategoryInput!) {
			//	createCategory(category: $category) // passing arguments from mutation to concrete createCategory mutation
			//	{
			//		id,
			//		name
			//		parent {
			//			id
			//		}
			//	}
			//}

			// arguments:
			//{
			//	"category": {
			//		"name": "MyName",
			//		"imageUrl": "myspecialurl",
			//		"parentId": 3
			//    }
			//}
			#endregion

			Field<CategoryType>("updateCategory")
				.Arguments(new QueryArguments(
					new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
					new QueryArgument<NonNullGraphType<CategoryInputType>> { Name = "category" }))
				.ResolveAsync(async ctx => {
					var id = ctx.GetArgument<int>("id");
					var inputModel = ctx.GetArgument<PostCategoryModel>("category");
					return await ctx.RequestServices.GetRequiredService<ICategoryService>().UpdateAsync(id, inputModel);
				});

			Field<CategoryType>("deleteCategory")
				.Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }))
				.ResolveAsync(async ctx => {
					var categoryId = ctx.GetArgument<int>("id");
					var outputCategory = await ctx.RequestServices
						.GetRequiredService<ICategoryService>()
						.DeleteAsync(categoryId);

					if (outputCategory != null) {
						var itemService = ctx.RequestServices.GetRequiredService<IProductService>();
						var itemsToDelete = await itemService.FindListAsync(new ItemQueryParams { CategoryId = categoryId });
						var deleteTasks = itemsToDelete.Select(x => itemService.DeleteAsync(x.Id));
						await Task.WhenAll(deleteTasks);
					}

					return outputCategory;
				});

			Field<ItemType>("createItem")
				.Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<ItemInputType>> { Name = "item" }))
				.ResolveAsync(async ctx => {
					var inputModel = ctx.GetArgument<PostItemModel>("item");
					return await ctx.RequestServices.GetRequiredService<IProductService>().CreateAsync(inputModel);
				});

			Field<ItemType>("updateItem")
				.Arguments(new QueryArguments(
					new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
					new QueryArgument<NonNullGraphType<ItemInputType>> { Name = "item" }))
				.ResolveAsync(async ctx => {
					var id = ctx.GetArgument<int>("id");
					var inputModel = ctx.GetArgument<PostItemModel>("item");
					return await ctx.RequestServices.GetRequiredService<IProductService>().UpdateAsync(id, inputModel);
				});

			Field<ItemType>("deleteItem")
				.Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }))
				.ResolveAsync(async ctx =>
					await ctx.RequestServices
						.GetRequiredService<IProductService>()
						.DeleteAsync(ctx.GetArgument<int>("id")));
		}
	}
}
