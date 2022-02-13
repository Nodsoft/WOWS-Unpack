using Microsoft.AspNetCore.Mvc;

namespace Nodsoft.WowsUnpack.Api.Controllers;

[ApiController, Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ReplayController : ControllerBase
{
	
	[HttpGet]
	public void Index()
	{
		
	}
}