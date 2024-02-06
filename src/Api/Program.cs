using parcelfy.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
	builder.Logging
		.AddConsole()
		.AddDebug();
}

// Add services to Configuration Appsettings
//builder.Services.Configure<LaPosteApiConfiguration>(builder.Configuration.GetSection(Constants.LaPoste));
// Api
builder.Services.AddApiServices(builder.Configuration);
// Application
builder.Services.AddApplicationServices();
// Infrastructure 
builder.Services.AddInfrastructureServices();


var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

[ExcludeFromCodeCoverage]
#pragma warning disable S1118 // Utility classes should not have public constructors
public partial class Program { }
#pragma warning restore S1118 // Utility classes should not have public constructors
