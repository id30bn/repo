using GraphQL.Types;

namespace GraphQLServer.Schema
{
	public class CategoryInputType : InputObjectGraphType
	{
		public CategoryInputType()
		{
			Name = "CategoryInput";
			Field<NonNullGraphType<StringGraphType>>("name");
			Field<IntGraphType>("parentId");
			Field<StringGraphType>("imageUrl");
		}
	}
}
