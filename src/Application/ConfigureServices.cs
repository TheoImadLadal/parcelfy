using parcelfy.Application.ParcelTrackers.Mappers;

namespace parcelfy.Application;
public static class ConfigureServices
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddTransient<IGetParcelTrackingDetailsById, GetParcelTrackingDetailsById>();

		var config = TypeAdapterConfig.GlobalSettings;
		config.Scan(Assembly.GetExecutingAssembly());

		services.AddSingleton(config);
		services.AddScoped<IMapper, ServiceMapper>();

		return services;
	}


}
