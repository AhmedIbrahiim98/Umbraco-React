using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Umbraco.Cms.Api.Management.OpenApi;
using Umbraco.Cms.Core.Composing;

namespace Umbraco_React.Samples;

public class MyItemApiComposer : IComposer
{
	public void Compose(IUmbracoBuilder builder)
	{
		builder.Services.ConfigureOptions<MyItemApiSwaggerGenOptions>();
	}
}

public class MyItemApiSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
{
	public void Configure(SwaggerGenOptions options)
	{
		// register the custom Swagger document "my-item-api"
		options.SwaggerDoc(
			"my-item-api",
			new OpenApiInfo { Title = "My item API", Version = "1.0" }
		);

		// enable Umbraco authentication for the "my-item-api" Swagger document
		options.OperationFilter<MyItemApiOperationSecurityFilter>();
	}
}

public class MyItemApiOperationSecurityFilter : BackOfficeSecurityRequirementsOperationFilterBase
{
	protected override string ApiName => "my-item-api";
}
