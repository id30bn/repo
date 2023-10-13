namespace Domain.SeedWork
{
	public interface IRepository<T> where T: Entity
	{
		Task<ICollection<T>> GetAllAsync();

		Task<T> GetByIdAsync(int id);

		T Add(T entity);

		Task<T> UpdateAsync(int id, T entity);

		void Delete(T entity);
	}
}
