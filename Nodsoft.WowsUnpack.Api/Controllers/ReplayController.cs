using Microsoft.AspNetCore.Mvc;
using Nodsoft.WowsUnpack.Api.Services;

namespace Nodsoft.WowsUnpack.Api.Controllers;

[ApiController, Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ReplayController : ControllerBase
{
	private readonly PythonRunner _runner;

	public ReplayController(PythonRunner runner)
	{
		_runner = runner;
	}
	
	
	[HttpGet]
	public async Task<string> Index()
	{
		const string cmd = @".\replay_parser.py --replay .\0.11.0.wowsreplay";
		//const string cmd = @"python .\replay_parser.py --replay .\0.11.0.wowsreplay";
		//const string cmd = @"echo $env:PATH";
		//const string cmd = @"pwd";

		return await _runner.RunScriptAsync(cmd);
	}
}