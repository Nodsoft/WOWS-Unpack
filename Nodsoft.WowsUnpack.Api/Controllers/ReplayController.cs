using Microsoft.AspNetCore.Mvc;
using Nodsoft.WowsUnpack.Api.Services;

namespace Nodsoft.WowsUnpack.Api.Controllers;

[ApiController, Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ReplayController : ControllerBase
{
	public const long MaximumReplaySize = 8388608;
	
	private readonly PythonReplayParser _replayParser;

	public ReplayController(PythonReplayParser replayParser)
	{
		_replayParser = replayParser;
	}
	
	
	[HttpPost, RequestSizeLimit(MaximumReplaySize)]
	public async Task<string> Index(IFormFile file, CancellationToken ct)
	{
		await using Stream stream = file.OpenReadStream();
		return await _replayParser.RunParserAsync(stream, ct);
	}
}