
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using WinWheel.Extensions;

namespace WinWheel
{
	public class Program
	{
		public static void Main(string[] args)
		{

			var builder = WebApplication.CreateBuilder(args);

			LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(),"/nlog.config"));


			builder.Services.ConfigureCors();
			builder.Services.ConfigureIISIntegration();

			builder.Services.ConfigureLoggerService();

			builder.Services.ConfigureRepositoryManager();
			builder.Services.ConfigureServiceManager();

			builder.Services.ConfigureSqlContext(builder.Configuration);

			builder.Services.AddAutoMapper(typeof(Program));
			// Add services to the container.

			builder.Services.AddControllers()
				.AddApplicationPart(typeof(WinWheel.Presentation.AssemblyReference).Assembly);

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
				app.UseDeveloperExceptionPage();
			else
				app.UseHsts();

			// Configure the HTTP request pipeline.

			app.UseHttpsRedirection();

			app.UseStaticFiles();
			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.All
			});
			app.UseCors("CorsPolicy");


			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
