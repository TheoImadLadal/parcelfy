namespace parcelfy.Infrastructure;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
	{
		return services
			.AddHttpClient()
			.AddScoped<IHttpParcelTrackingRepository, HttpParcelTrackersRepository>()
			.AddScoped<IInMemoryParcelTrackingRepository, InMemoryParcelTrackersRepository>()
			.AddDbContext<ParcelfyDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(Constants.AzureDb)));

	}
}