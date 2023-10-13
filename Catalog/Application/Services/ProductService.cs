using Application.Interfaces;
using Application.Models;
using Domain.Exceptions;
using Domain.SeedWork;

namespace Application.Services
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _uof;
		private readonly IDtoMapper _mapper;
		public ProductService(IUnitOfWork uof, IDtoMapper mapper)
		{
			_uof = uof;
			_mapper = mapper;
		}

		public async Task<Product> CreateAsync(Product product)
		{
			var category = await _uof.CategoryRepository.GetByIdAsync(product.CategoryId);
			if (category == null) {
				throw new CategoryNotFoundException($"Cannot find category by id {product.CategoryId}");
			}

			var domain = _uof.ItemRepository.Add(_mapper.MapToDomainProduct(product));
			await _uof.CommitAsync();
			return _mapper.MapToProductDTO(domain);
		}

		public async Task<Product> DeleteAsync(int id)
		{
			var domain = await _uof.ItemRepository.GetByIdAsync(id);
			if (domain != null) {
				_uof.ItemRepository.Delete(domain);
				await _uof.CommitAsync();
			}

			return _mapper.MapToProductDTO(domain);
		}

		public async Task<Product> GetByIdAsync(int id)
		{
			return _mapper.MapToProductDTO(await _uof.ItemRepository.GetByIdAsync(id));
		}

		public async Task<IEnumerable<Product>> ListAsync()
		{
			var domains = await _uof.ItemRepository.GetAllAsync();
			return domains.Select(x => _mapper.MapToProductDTO(x));
		}

		public async Task<Product> UpdateAsync(int id, Product product)
		{
			var category = await _uof.CategoryRepository.GetByIdAsync(product.CategoryId);
			if (category == null) {
				throw new CategoryNotFoundException($"Cannot find category by id {product.CategoryId}");
			}

			var result = await _uof.ItemRepository.UpdateAsync(id, _mapper.MapToDomainProduct(product));
			if (result != null) {
				await _uof.CommitAsync();
			}
			return _mapper.MapToProductDTO(result);
		}
	}
}
