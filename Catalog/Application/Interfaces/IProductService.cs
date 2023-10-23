using Application.Models;

namespace Application.Interfaces
{
	public interface IProductService
	{
		Task<IEnumerable<Product>> ListAsync();

		Task<Product> GetByIdAsync(int id);

		Task<Product> CreateAsync(Product product);

		Task<Product> UpdateAsync(int id, Product product);

		Task<Product> DeleteAsync(int id);
	}
}
