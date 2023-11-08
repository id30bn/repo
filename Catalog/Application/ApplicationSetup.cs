using Application.Interfaces;
using Application.Models.Mappers;
using Application.Services;
using MessageBroker.Shared;
using MessageBroker.Shared.Interfaces;
using MessageBroker.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
	public static class ApplicationSetup
	{
		public static IServiceCollection ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IProductService, ProductService>();

			var messageBusSection = configuration.GetSection("RabbitMq");
			services.AddScoped<IRabbitMqSenderService>(sp =>
				new RabbitMqSenderService(configuration.GetConnectionString("RabbitMq"),
					messageBusSection.GetValue<string>("ExchangeName")));
			services.AddScoped<INotificationService, NotificationService>();

			services.AddAutoMapper(typeof(CategoryMapperProfile).Assembly);

			return services;
		}
	}
}
