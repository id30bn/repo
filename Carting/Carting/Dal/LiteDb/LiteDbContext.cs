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

			// comment these lines if you don't want to handle separate Items collection (cascade deletion etc.)
			var mapper = BsonMapper.Global;
			mapper.Entity<Cart>()
				.DbRef(x => x.Items);
		}
	}
}
