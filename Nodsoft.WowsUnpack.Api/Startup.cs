using System.Net;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Nodsoft.WowsUnpack.Api.Infrastructure.Middlewares;
using Nodsoft.WowsUnpack.Api.Infrastructure.Swagger;
using Nodsoft.WowsUnpack.Api.Services;
using Swashbuckle.AspNetCore.SwaggerGen;

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
		
		services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

		services.AddSwaggerGen(options =>
		{
			options.OperationFilter<SwaggerDefaultValues>();

			// Set the comments path for the Swagger JSON and UI.
			string xmlFile = $"{typeof(Startup).Assembly.GetName().Name}.xml";
			string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
			options.IncludeXmlComments(xmlPath);

			// Bearer token authentication
			options.AddSecurityDefinition("jwt_auth", new()
			{
				Name = "bearer",
				BearerFormat = "JWT",
				Scheme = "bearer",
				Description = "Specify the authorization token.",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.Http,
			});

			/*
			// Make sure swagger UI requires a Bearer token specified
			OpenApiSecurityScheme securityScheme = new()
			{
				Reference = new()
				{
					Id = "jwt_auth",
					Type = ReferenceType.SecurityScheme
				}
			};

			options.AddSecurityRequirement(new()
			{
				{ securityScheme, Array.Empty<string>() },
			});
			*/
		});
		
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();

		services.AddSingleton<PythonRunner>();
	}

	
	public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
	{
		app.UseSwagger(options => { options.RouteTemplate = "swagger/{documentName}/swagger.json"; });
		app.UseSwaggerUI(options =>
		{
			options.RoutePrefix = "swagger";

			foreach (ApiVersionDescription description in provider.ApiVersionDescriptions.Reverse())
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
			/*
			 * Remove once a website is built.
			 */
			endpoints.MapGet("/", context =>
			{
				context.Response.Redirect("/swagger/index.html");
				return Task.CompletedTask;
			});
			
			endpoints.MapControllers();
		});
	}
}