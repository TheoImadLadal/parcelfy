using FluentValidation;
using parcelfy.Application.Services;
using parcelfy.Application.UseCases;
using parcelfy.Core.Validators;

namespace parcelfy.Application;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddScoped<IParcelTrackingService, ParcelTrackingService>();
		services.AddScoped<IGetParcelTrackingDetails, GetParcelTrackingDetails>();
		services.AddScoped<ICreateParcelTrackingDetails, CreateParcelTrackingDetails>();

		services.AddScoped<IValidator<ParcelTrackerHistoryDTO>, ParcelTrackerHistoryValidator>();

		var config = TypeAdapterConfig.GlobalSettings;
		config.Scan(Assembly.GetExecutingAssembly());

		services.AddSingleton(config);
		services.AddScoped<IMapper, ServiceMapper>();

		return services;
	}


}
