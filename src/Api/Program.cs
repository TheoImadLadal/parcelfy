using Microsoft.Extensions.Diagnostics.HealthChecks;
using parcelfy.Application.ParcelTrackers.Abstractions;
using parcelfy.Application.ParcelTrackers.Queries.GetTrackingFromParcelId;
using parcelfy.Application.WeatherForecasts.Abstractions;
using parcelfy.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using parcelfy.Infrastructure;
using parcelfy.Infrastructure.ParcelTrackersRepository;
using parcelfy.Infrastructure.ParcelTrackersRepository.Abstractions;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Logging
        .AddConsole()
        .AddDebug();
}

// Add services to the container.
builder.Services.AddControllers();
// Add heatlcheck
builder.Services.AddHealthChecks().AddCheck("Default", ()=> HealthCheckResult.Healthy("OK"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add services to Configuration Appsettings
builder.Services.Configure<LaPosteApiConfiguration>(builder.Configuration.GetSection(Constants.LaPoste));
builder.Services.AddHttpClient();
// Add services 
// APPLICATION
builder.Services.AddSingleton<IGetWeatherForecastQuery, GetWeatherForecastsQuery>();
builder.Services.AddSingleton<IGetTrackingFromParcelId, GetTrackingFromParcelId>();
// INFRA
builder.Services.AddSingleton<IParcelTrackingRepository, ParcelTrackersRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
