using Serilog;
using Serilog.Events;

namespace Nodsoft.WowsUnpack.Api;

public static class Program
{
	public static async Task Main(string[] args)
	{
		using IHost host = CreateHostBuilder(args).Build();
		IConfiguration configuration = host.Services.GetRequiredService<IConfiguration>();
		
		Log.Logger = new LoggerConfiguration()
			.ReadFrom.Configuration(configuration)
			.CreateLogger();

		await host.RunAsync();
	}
	
	
	public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.UseSerilog()
			.ConfigureWebHostDefaults(webBuilder =>
			{
				webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
				{
					config.SetBasePath(Directory.GetCurrentDirectory());
					config.AddJsonFile("appsettings.json", true, true)
						.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
					config.AddEnvironmentVariables();
					config.AddCommandLine(args);
				});

				webBuilder.UseStartup<Startup>();
			});
}