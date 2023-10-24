using Domain.SeedWork;
using System.Linq.Expressions;

namespace Domain.CategoryAggregate
{
	public interface IItemRepository : IRepository<Item>
	{
		Task<List<Item>> FindListAsync(Expression<Func<Item, bool>> expression, int skip, int take);
	}
}
