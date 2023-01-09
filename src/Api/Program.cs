var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
	builder.Logging
		.AddConsole()
		.AddDebug();
}

// Add services to Configuration Appsettings
builder.Services.Configure<LaPosteApiConfiguration>(builder.Configuration.GetSection(Constants.LaPoste));
// Api
builder.Services.AddApiServices();
// Application
builder.Services.AddApplicationServices();
// Infrastructure 
builder.Services.AddInfrastructureServices(builder.Configuration);


var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
