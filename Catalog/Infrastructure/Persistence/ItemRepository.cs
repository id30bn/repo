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

		public override async Task<Item> UpdateAsync(int id, Item updatedEntity)
		{
			var domain = await GetByIdAsync(id);
			if (domain == null) {
				return null;
			}

			updatedEntity.Id = domain.Id;

			// for owned entities
			domain.SetImage(updatedEntity.Image?.Url);
			domain.SetDescription(updatedEntity.Description?.Text);

			// for other properties
			_context.Entry(domain).CurrentValues.SetValues(updatedEntity);

			return updatedEntity;
		}
	}
}
