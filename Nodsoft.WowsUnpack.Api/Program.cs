namespace Nodsoft.WowsUnpack.Api;

public static class Program
{
	public static async Task Main(string[] args)
	{
		using IHost host = CreateHostBuilder(args).Build();
		
		await host.RunAsync();
	}
	
	
	public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(webBuilder =>
			{
				webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
				{
					config.SetBasePath(Directory.GetCurrentDirectory());
					config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
						.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
					config.AddEnvironmentVariables();
					config.AddCommandLine(args);
				});

				webBuilder.UseStartup<Startup>();
				//webBuilder.UseSerilog(); 
			});
}