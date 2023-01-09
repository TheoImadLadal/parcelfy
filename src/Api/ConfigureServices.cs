﻿namespace parcelfy.Api;

public static class ConfigureServices
{
	public static IServiceCollection AddApiServices(this IServiceCollection services)
	{
		// Add services to the container.
		services.AddControllers()
			.AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
			});
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo
		{
			Version = "v1",
			Title = "Parcelfy API",
			Description = "<b>A Dotnet Core Web API for tracking details <i>(Only Colissimo, Chronopost and LaPoste for the moment).</i></b><br/>",
			Contact = new OpenApiContact
			{
				Name = "Theo Imad Ladal",
				Url = new Uri($"https://github.com/TheoImadLadal/")
			}
		}));
		services.AddResponseCompression();
		services.AddDataProtection();
		return services;

	}
}