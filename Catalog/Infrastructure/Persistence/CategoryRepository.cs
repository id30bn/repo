using Domain.CategoryAggregate;
 
namespace Infrastructure.Persistence
{
	public class CategoryRepository : EfBaseRepository<Category>, ICategoryRepository
	{
		public CategoryRepository(AppDbContext context) : base(context) { }

		public override async Task<Category> UpdateAsync(int id, Category updatedEntity)
		{
			var domain = await GetByIdAsync(id);
			if (domain == null) {
				return null;
			}

			updatedEntity.Id = domain.Id;

			// for owned entities
			domain.SetImage(updatedEntity.Image?.Url);

			// for other properties
			_context.Entry(domain).CurrentValues.SetValues(updatedEntity);

			return updatedEntity;
		}
	}
}
