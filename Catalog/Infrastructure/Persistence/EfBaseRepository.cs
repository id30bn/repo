using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
	public abstract class EfBaseRepository<TEntity> : IRepository<TEntity>
		where TEntity: Entity
	{
		public readonly AppDbContext _context;

		public EfBaseRepository(AppDbContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public async Task<ICollection<TEntity>> GetAllAsync()
		{
			return await _context.Set<TEntity>().ToListAsync();
		}

		public async Task<TEntity> GetByIdAsync(int id) {
			return await _context.Set<TEntity>().FindAsync(id);
		}

		public TEntity Add(TEntity entity)
		{
			_context.Set<TEntity>().Add(entity);
			return entity;
		}

		public void Delete(TEntity entity)
		{
			_context.Set<TEntity>().Remove(entity);
		}

		public virtual async Task<TEntity> UpdateAsync(int id, TEntity entity)
		{
			var domain = await GetByIdAsync(id);
			if (domain == null) {
				return null;
			}

			_context.Entry(domain).State = EntityState.Detached;
			entity.Id = domain.Id;
			_context.Entry(entity).State = EntityState.Modified;

			//_context.Entry(domain).CurrentValues.SetValues(entity);

			return entity;
		}
	}
}
