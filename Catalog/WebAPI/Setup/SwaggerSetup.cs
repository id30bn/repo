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

				setup.AddSecurityDefinition("Bearer",
					new OpenApiSecurityScheme {
						In = ParameterLocation.Header,
						Description = "Please enter into field the word 'Bearer' following by space and JWT",
						Name = "Authorization",
						Type = SecuritySchemeType.ApiKey
					});
				setup.AddSecurityRequirement(new OpenApiSecurityRequirement()
					{
					{
					new OpenApiSecurityScheme()
					{
						Reference = new OpenApiReference()
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						},
						Scheme = "Bearer",
						Type = SecuritySchemeType.Http,
						Name = "Bearer",
						In = ParameterLocation.Header
					}, new List<string>()
					}
				});
			});
		}
	}
}
