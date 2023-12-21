using GraphQLServer;
using Application;
using Infrastructure;
using GraphQL.Types;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.ConfigureApplication(builder.Configuration);
builder.Services.RegisterGraphQLDependencies(builder.Configuration);

var app = builder.Build();


app.UseHttpsRedirection();

app.UseGraphQL<ISchema>("/graphql");
app.UseGraphQLPlayground(
	"/",
	new GraphQL.Server.Ui.Playground.PlaygroundOptions {
		GraphQLEndPoint = "/graphql",
		SubscriptionsEndPoint = "/graphql",
	});

app.UseGraphQLGraphiQL();

app.Run();
