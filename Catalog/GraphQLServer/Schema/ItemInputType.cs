using GraphQL.Types;

namespace GraphQLServer.Schema
{
	public class ItemInputType : InputObjectGraphType
	{
		public ItemInputType() {
			Name = "ItemInput";
			Field<NonNullGraphType<StringGraphType>>("name");
			Field<StringGraphType>("description");
			Field<StringGraphType>("imageUrl");
			Field<NonNullGraphType<IntGraphType>>("categoryId");
			Field<NonNullGraphType<IntGraphType>>("price");
			Field<NonNullGraphType<IntGraphType>>("amount");
		}
	}
}
