using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Carting.Core.Interfaces;
using Carting.Dal.LiteDb;
using Carting.Services;
using Carting.Services.MessageBroker;
using Carting.Setup;
using MessageBroker.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication("Bearer")
	.AddJwtBearer("Bearer", config => { // or use AddOAuth
		config.RequireHttpsMetadata = false;
		config.MetadataAddress = "http://localhost:8080/realms/course/.well-known/openid-configuration";
		config.Audience = "carting_client_id"; //carting
		config.SaveToken = true;
		config.Events = new JwtBearerEvents();
		config.Events.OnTokenValidated = ctx => {
			// log ctx.SecurityToken here

			ClaimsIdentity claimsIdentity = (ClaimsIdentity)ctx.Principal.Identity;
			var knownRoles = new List<string> { "Manager", "Buyer" };

			if (claimsIdentity.IsAuthenticated && claimsIdentity.HasClaim(claim => claim.Type == "realm_access")) {
				var realmClaim = claimsIdentity.FindFirst(claim => claim.Type == "realm_access");
				var realmClaimAsDictionary = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(realmClaim.Value);
				var roles = realmClaimAsDictionary["roles"];
				foreach (var role in roles) {
					if (knownRoles.Contains(role)) {
						claimsIdentity.AddClaim(new Claim("customTypeNameRole", role));
					}
				}
			}

			return Task.CompletedTask;
		};
	});

builder.Services.AddAuthorization(opts => {
	opts.AddPolicy("OnlyForManagers", policy => {
		policy.RequireClaim("customTypeNameRole", "Manager"); //ClaimsIdentity.DefaultRoleClaimType
	});
	opts.AddPolicy("OnlyForBuyers", policy => {
		policy.RequireClaim("customTypeNameRole", "Buyer");
	});
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var apiVersioningBuilder = builder.Services.AddApiVersioning(config => {
	config.DefaultApiVersion = new ApiVersion(1, 0);
	config.ReportApiVersions = true;
	config.AssumeDefaultVersionWhenUnspecified = true;
	config.ApiVersionReader = new UrlSegmentApiVersionReader();
});

apiVersioningBuilder.AddApiExplorer(options => {
	//adds IApiVersionDescriptionProvider service (needed to configure swagger)

	// the specified format code will format the version as "'v'major[.minor][-status]"
	options.GroupNameFormat = "'v'VVV";

	// this option is only necessary when versioning by url segment. the SubstitutionFormat
	// can also be used to control the format of the API version in route templates
	options.SubstituteApiVersionInUrl = true;
}); // Nuget Package: Asp.Versioning.Mvc.ApiExplorer

builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

builder.Services.Configure<LiteDbOptions>(builder.Configuration.GetSection("LiteDbOptions"));
builder.Services.AddSingleton<ILiteDbContext, LiteDbContext>();

builder.Services.AddScoped<ICartingRepository, CartingRepository>();
builder.Services.AddScoped<ICartingService, CartingService>();

//builder.Services.AddHostedService(serviceProvider =>
//	new RabbitMqListener("item", channel => new ItemMessageConsumer(channel, serviceProvider.GetService<ICartingService>()))
//);

//builder.Services.AddScoped<IRabbitMqReceiverService, RabbitMqReceiverService>(serviceProvider =>
//	new RabbitMqReceiverService("item", channel => new ItemMessageConsumer(channel, serviceProvider.GetService<ICartingService>()))
//);

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.AddScoped<IRabbitMqFactory, RabbitMqFactory>();

builder.Services.AddHostedService(serviceProvider => {
	using (var scope = serviceProvider.CreateScope()) {
		var rabbitMqFactory = scope.ServiceProvider.GetRequiredService<IRabbitMqFactory>();
		return new CatalogListenerService(rabbitMqFactory);
	}
});

var app = builder.Build();
app.UseAuthentication();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

	app.UseSwagger();
	app.UseSwaggerUI(options => {
		foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions) {
			options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
				description.GroupName.ToUpperInvariant());
		}
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
