namespace parcelfy.Api;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
	public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
	{
		// Add services to the container.
		services.AddControllers()
			.AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(options =>
		{
			options.SwaggerDoc("v1", new OpenApiInfo
			{
				Version = "v1",
				Title = "Parcelfy API",
				Description = "<b>A Dotnet Core Web API for tracking details <i>(Only Colissimo, Chronopost and LaPoste for the moment).</i></b><br/>",
				Contact = new OpenApiContact
				{
					Name = "Theo Imad Ladal",
				}
			});
			options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				In = ParameterLocation.Header,
				Description = "Please enter a valid token",
				Name = "Authorization",
				Type = SecuritySchemeType.Http,
				BearerFormat = "JWT",
				Scheme = "Bearer"
			});
			options.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type=ReferenceType.SecurityScheme,
							Id="Bearer"
						}
					},
					new string[]{}
				}
			});

			// Set the comments path for the Swagger JSON and UI.
			var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
			options.IncludeXmlComments(xmlPath);
		});

		// Add authnetication
		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		.AddJwtBearer( options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidIssuer = configuration["Authentication:Issuer"],
				ValidAudience = configuration["Authentication:Audience"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:Key"])),
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true
			};
		});

		services.AddScoped<IValidator<ParcelTrackerHistory>, ParcelTrackerHistoryValidator>();
		services.AddResponseCompression();
		services.AddDataProtection();
		return services;

	}
}
