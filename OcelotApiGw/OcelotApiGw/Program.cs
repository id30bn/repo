using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("ocelot.json");

//var identityUrl = _cfg.GetValue<string>("IdentityUrl"); //from config 
//var authenticationProviderKey = "IdentityApiKey"; // use it instead of "Bearer"

//builder.Services.AddAuthentication("Bearer")
//	.AddJwtBearer("Bearer", config => { // or use AddOAuth
//		config.RequireHttpsMetadata = false;
//		config.MetadataAddress = "http://localhost:8080/realms/course/.well-known/openid-configuration";
//		config.Audience = "catalog";
//		config.Authority = identityUrl;
//		config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters() {
//			ValidAudiences = new[] { "catalog", "carting" }
//		};
//	});

builder.Services.AddOcelot(builder.Configuration);
	//.AddTransientDefinedAggregator<FakeDefinedAggregator>();

var app = builder.Build();

app.UseOcelot().Wait();

app.Run();
