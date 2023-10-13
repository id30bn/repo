using Domain.CategoryAggregate;

namespace Infrastructure.Persistence
{
	public class ItemRepository : EfBaseRepository<Item>, IItemRepository
	{
		public ItemRepository(AppDbContext context) : base(context) { }
	}
}
