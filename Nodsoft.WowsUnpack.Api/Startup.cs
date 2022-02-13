using System.Net;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
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
		
		services.AddApiVersioning(config =>
		{
			config.DefaultApiVersion = new(3, 0, "beta");
			config.AssumeDefaultVersionWhenUnspecified = true;
			config.ReportApiVersions = true;
		});

		services.AddVersionedApiExplorer(options =>
		{
			options.GroupNameFormat = "'v'VVV";
			options.SubstituteApiVersionInUrl = true;
		});
		
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
	{
		app.UseSwagger(options => { options.RouteTemplate = "swagger/{documentName}/swagger.json"; });
		app.UseSwaggerUI(options =>
		{
			options.RoutePrefix = "swagger";

			foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
			{
				options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToLowerInvariant());
			}
		});

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