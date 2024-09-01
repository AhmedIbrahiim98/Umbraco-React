using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Umbraco.Cms.Api.Management.OpenApi;
using Umbraco.Cms.Core.Composing;

namespace Umbraco_React.Samples;

public class HomePageApiComposer : IComposer
{
	public void Compose(IUmbracoBuilder builder)
	{
		builder.Services.ConfigureOptions<HomePageApiSwaggerGenOptions>();
	}
}

public class HomePageApiSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
{
	public void Configure(SwaggerGenOptions options)
	{
		// register the custom Swagger document "home-page-api"
		options.SwaggerDoc(
			"home-page-api",
			new OpenApiInfo { Title = "Home page API", Version = "1.0" }
		);

		// enable Umbraco authentication for the "home-page-api" Swagger document
		options.OperationFilter<HomePageApiOperationSecurityFilter>();
	}
}

public class HomePageApiOperationSecurityFilter : BackOfficeSecurityRequirementsOperationFilterBase
{
	protected override string ApiName => "home-page-api";
}
