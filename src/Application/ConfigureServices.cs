namespace parcelfy.Application;
public static class ConfigureServices
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddTransient<IGetParcelTrackingDetailsById, GetParcelTrackingDetailsById>();
		services.AddTransient<ICreateParcelTrackingDetailsCommands, CreateParcelTrackingDetailsCommand>();

		var config = TypeAdapterConfig.GlobalSettings;
		config.Scan(Assembly.GetExecutingAssembly());

		services.AddSingleton(config);
		services.AddScoped<IMapper, ServiceMapper>();

		return services;
	}


}
