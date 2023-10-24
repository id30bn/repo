using Domain.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence
{
	public class ItemRepository : EfBaseRepository<Item>, IItemRepository
	{
		public ItemRepository(AppDbContext context) : base(context) { }

		public Task<List<Item>> FindListAsync(Expression<Func<Item, bool>> expression, int skip, int take)
		{
			return _context.Item
				.Where(expression)
				.Skip(skip)
				.Take(take)
				.ToListAsync();
		}
	}
}
