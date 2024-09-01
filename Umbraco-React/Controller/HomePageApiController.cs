using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Api.Common.Attributes;
using Umbraco.Cms.Api.Common.ViewModels.Pagination;
using Umbraco.Cms.Api.Management.Controllers;
using Umbraco.Cms.Api.Management.Routing;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Umbraco_React.Controller;

[AllowAnonymous]  //If you want your API endpoint to be accessible without authentication
//[Authorize]
//[Route("umbraco/management/api/[controller]")]  //Add Custom Route
[VersionedApiBackOfficeRoute("/")]
//With the ApiExplorerSettings attribute, we can put all our endpoints into a given group. This is a nice way of organizing our endpoints in the Swagger UI.
[ApiExplorerSettings(GroupName = "Home Page API")]
[MapToApi("home-page-api")]
public class HomePageApiController : ManagementApiControllerBase
{
	private readonly IPublishedContentQuery _publishedContentQuery;
	private readonly IContentService _contentService;

	public HomePageApiController(
		IPublishedContentQuery publishedContentQuery,
		IContentService contentService)
	{
		_publishedContentQuery = publishedContentQuery;
		_contentService = contentService;
	}

	[HttpGet("homePage")]
	public IActionResult GetHomePage()
	{
		// Find the homepage by its content type alias (replace "homePage" with your actual alias)
		var homePageId = 1057;

		var homePage = _publishedContentQuery.Content(homePageId) as HomePage;
		// If no homepage is found, return 404
		if (homePage == null)
		{
			return NotFound("Sorry....Homepage content not found.");
		}

		var homepageData = new
		{
			id = homePage.Id,
			name = homePage.Name,
			Url = homePage.Url(),
			//Title = homePage.Value("title"),
			//Description = homePage.Value<string>("description"),
			properties = homePage.Properties.ToDictionary(x => x.Alias, x => x.GetValue())
		};

		return Ok(homepageData);
	}
}
