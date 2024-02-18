namespace parcelfy.Infrastructure;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
	{
		services
			.AddHttpClient()
			.AddScoped<IHttpParcelTrackingRepository, HttpParcelTrackersRepository>()
			.AddScoped<IInMemoryParcelTrackingRepository, InMemoryParcelTrackersRepository>()
			.AddDbContext<ParcelfyDbContext>(options => options.UseNpgsql(Constants.PostgresDb));
		//.AddDbContext<ParcelfyDbContext>(options => options.UseSqlServer(Constants.AzureDb));

		return services;
	}
}