using Domain.SeedWork;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
	public static class Dependencies
	{
		public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(
					configuration.GetConnectionString("DefaultConnection"),
					b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

			//services.AddScoped<IItemRepository, ItemRepository>();
			//services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}
	}
}
