using System.Net;
using Microsoft.AspNetCore.HttpOverrides;
using Nodsoft.WowsUnpack.Api.Infrastructure.Middlewares;

namespace Nodsoft.WowsUnpack.Api;

public class Startup
{
	public IConfiguration Configuration { get; }

	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddControllers();
		
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		// Configure the HTTP request pipeline.
		//app.UseResponseCompression();

		app.UseSwagger();
		app.UseSwaggerUI();

		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		app.UseRouting();

		IPAddress[]? allowedProxies = Configuration.GetSection("AllowedProxies")?.Get<string[]>()?.Select(IPAddress.Parse).ToArray();

		// Nginx configuration step
		ForwardedHeadersOptions forwardedHeadersOptions = new()
		{
			ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
		};

		if (allowedProxies is { Length: > 0 })
		{
			forwardedHeadersOptions.KnownProxies.Clear();

			foreach (IPAddress address in allowedProxies)
			{
				forwardedHeadersOptions.KnownProxies.Add(address);
			}
		}

		app.UseForwardedHeaders(forwardedHeadersOptions);
		
		app.UseMiddleware<RequestLoggingMiddleware>();
		
		app.UseEndpoints(endpoints =>
		{
			endpoints.MapDefaultControllerRoute();
		});
	}
}