using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Nodsoft.WowsUnpack.Api.Services;
using Nodsoft.WowsUnpack.Common.Data.DTOs;

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

	/// <summary>
	/// Lists current headers
	/// </summary>
	[HttpHead]
	public void ListHeaders()
	{
		
	}
	
	/// <summary>
	/// Submits a replay for processing.
	/// </summary>
	/// <param name="file">Replay file to be used</param>
	/// <param name="ct">Cancellation token</param>
	/// <returns>Unpacked data from the Replay file, in JSON Format.</returns>
	[HttpPost, RequestSizeLimit(MaximumReplaySize)]
	[ProducesResponseType(typeof(JsonReplayDto), 200)]
	public async Task<IActionResult> Index(IFormFile file, CancellationToken ct)
	{
		await using Stream stream = file.OpenReadStream();
		
		JsonNode? json = JsonNode.Parse(
			await _replayParser.RunParserAsync(stream, ct));

		return new JsonResult(json, new JsonSerializerOptions
		{
			WriteIndented = !Request.Headers.Accept.Contains("application/json") && Request.Headers.Accept.Contains("text/json")
		});
	}
}