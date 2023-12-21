using GraphQL;
using GraphQL.Types;
using GraphQLServer.Schema;

namespace GraphQLServer
{
	public static class Dependencies
	{
		public static void RegisterGraphQLDependencies(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<ItemType>();
			services.AddSingleton<CategoryType>();
			services.AddSingleton<CategoryInputType>();
			services.AddSingleton<ItemInputType>();
			services.AddSingleton<RootQuery>();
			services.AddSingleton<RootMutation>();
			services.AddSingleton<ISchema, CatalogSchema>();

			services.AddGraphQL(x => x.AddAutoSchema<CatalogSchema>()
				.AddSystemTextJson());
		}
	}
}
