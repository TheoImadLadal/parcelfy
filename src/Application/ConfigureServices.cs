namespace parcelfy.Application;
public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IGetParcelTrackingDetailsById, GetParcelTrackingDetailsById>();
    }
}
