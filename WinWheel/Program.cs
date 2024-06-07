
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



			// Add services to the container.

			builder.Services.AddControllers();

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