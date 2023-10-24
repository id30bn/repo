using Microsoft.OpenApi.Models;
using System.Reflection;

namespace WebAPI.Setup
{
	internal static class SwaggerSetup
	{
		internal static IServiceCollection ConfigureSwagger(this IServiceCollection services)
		{
			return services.AddSwaggerGen(setup => {
				setup.SwaggerDoc(
					"v1",
					new OpenApiInfo {
						Title = "Catalog Api",
						Version = "v1",
						Description = "An API for catalog service"
					});

				var outputDocXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var filePath = Path.Combine(AppContext.BaseDirectory, outputDocXmlFile);
				setup.IncludeXmlComments(filePath);
			});
		}
	}
}
