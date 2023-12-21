namespace GraphQLServer.Schema
{
	public class CatalogSchema : GraphQL.Types.Schema
	{
		public CatalogSchema(IServiceProvider provider)
			: base(provider)
		{
			Query = provider.GetRequiredService<RootQuery>();
			Mutation = provider.GetRequiredService<RootMutation>();
		}
	}
}
