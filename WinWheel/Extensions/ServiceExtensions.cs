using Contracts;
using LoggerService;
using Repository;
using Service.Contracts;
using Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace WinWheel.Extensions
{
	public static class ServiceExtensions
	{
		public static void ConfigureIdentity(this IServiceCollection services)
		{
			services.AddIdentityCore<Player>(options =>
			{				
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 0;
			

			})
			.AddEntityFrameworkStores<RepositoryContext>()
			.AddDefaultTokenProviders()			
			.AddEntityFrameworkStores<RepositoryContext>();
		}
		public static void ConfigureCors(this IServiceCollection services) =>
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", builder =>
				builder.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());
			});

		public static void ConfigureIISIntegration(this IServiceCollection services) =>
			services.Configure<IISOptions>(options =>
			{
			});

		public static void ConfigureLoggerService(this IServiceCollection services) =>
			services.AddSingleton<ILoggerManager, LoggerManager>();

		//Repository Manager
		public static void ConfigureRepositoryManager(this IServiceCollection services) =>
			services.AddScoped<IRepositoryManager, RepositoryManager>();

		public static void ConfigureServiceManager(this IServiceCollection services) =>
			services.AddScoped<IServiceManager, ServiceManager>();

		public static void ConfigureSqlContext(this IServiceCollection services,
			IConfiguration configuration) =>
			services.AddDbContext<RepositoryContext>(opts =>
			opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

		//Caching services
		public static void ConfigureResponseCaching(this IServiceCollection services) =>
			services.AddResponseCaching();

		public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
		{
			var jwtSettings = configuration.GetSection("JwtSettings");
			var secretKey = Environment.GetEnvironmentVariable("SECRET");

			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,

					ValidIssuer = jwtSettings["validIssuer"],
					ValidAudience = jwtSettings["validAudience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
				};
			});
		}

		public static void ConfigureSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(s =>
			{
				s.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Win Wheel API",
					Version = "v1",
					Description = "Win Wheel API para Proyecto Unilink",
					TermsOfService = new Uri("https://www.unilinkcorp.com/"),
					Contact = new OpenApiContact
					{
						Name = "Osiris Román",
						Email = "osiris.roman@ulinksolution.com",				

					},
					License = new OpenApiLicense
					{
						Name = "Unilink - Nueva Providencia N°1945 oficina 502 - Providencia, CP 7500503. CHILE.",
						Url = new Uri("https://www.unilinkcorp.com/"),
					}
				});

				var xmlFile = $"{typeof(Presentation.AssemblyReference).Assembly.GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				s.IncludeXmlComments(xmlPath);

				s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Place to add JWT with Bearer",
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});

				s.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							},
							Name = "Bearer",
						},
						new List<string>()
					}
				});


			});

			
		}

	}
}
