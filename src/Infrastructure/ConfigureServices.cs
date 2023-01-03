namespace parcelfy.Infrastructure;

public static class ConfigureServices
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
	{
		return services
			.AddHttpClient()
			.AddScoped<IHttpParcelTrackingRepository, HttpParcelTrackersRepository>();
	}
}