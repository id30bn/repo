using Carting.Core.Models.Cart;
using LiteDB;
using Microsoft.Extensions.Options;

namespace Carting.Dal.LiteDb
{
	public class LiteDbContext : ILiteDbContext
	{
		public LiteDatabase Database { get; }

		public LiteDbContext(IOptions<LiteDbOptions> options)
		{
			Database = new LiteDatabase(options.Value.DatabaseLocation);

			// you could have only one Carts collection without the need for handling cascade deletion etc.
			// also these lines are needed for the "Include" logic

			var mapper = BsonMapper.Global;
			mapper.Entity<Cart>()
				.DbRef(x => x.Items);
		}
	}
}
