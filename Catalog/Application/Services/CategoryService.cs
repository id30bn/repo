using Application.Interfaces;
using Application.Models;
using Domain.Exceptions;
using Domain.SeedWork;

namespace Application.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly IUnitOfWork _uof;
		private readonly IDtoMapper _mapper;
		public CategoryService(IUnitOfWork uof, IDtoMapper mapper)
		{
			_uof = uof;
			_mapper = mapper;
		}

		public async Task<CategoryDTO> CreateAsync(CategoryDTO category)
		{
			var domain = _uof.CategoryRepository.Add(_mapper.MapToDomainCategory(category));
			await _uof.CommitAsync();
			return _mapper.MapToCategoryDTO(domain);
		}

		public async Task<CategoryDTO> DeleteAsync(int id)
		{
			var domain = await _uof.CategoryRepository.GetByIdAsync(id);
			if (domain != null) {
				_uof.CategoryRepository.Delete(domain);
				await _uof.CommitAsync();
			}
		 
			return _mapper.MapToCategoryDTO(domain);
		}

		public async Task<CategoryDTO> GetByIdAsync(int id)
		{
			return _mapper.MapToCategoryDTO(await _uof.CategoryRepository.GetByIdAsync(id));
		}

		public async Task<IEnumerable<CategoryDTO>> ListAsync()
		{
			var domains = await _uof.CategoryRepository.GetAllAsync();
			return domains.Select(x => _mapper.MapToCategoryDTO(x)).ToList();
		}

		public async Task<CategoryDTO> UpdateAsync(int id, CategoryDTO category)
		{
			var result = await _uof.CategoryRepository.UpdateAsync(id, _mapper.MapToDomainCategory(category));
			if (result != null) {
				await _uof.CommitAsync();
			}
			return _mapper.MapToCategoryDTO(result);
		}
	}
}
