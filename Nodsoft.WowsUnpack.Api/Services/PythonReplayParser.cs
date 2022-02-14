using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Nodsoft.WowsUnpack.Api.Services;

/// <summary>
/// POC class to experiment with Python processes
/// </summary>
public class PythonReplayParser
{
	private readonly ILogger<PythonReplayParser> _logger;
	private readonly IConfiguration _configuration;

	public PythonReplayParser(ILogger<PythonReplayParser> logger, IConfiguration configuration)
	{
		_logger = logger;
		_configuration = configuration;
	}

	public async Task<string> RunParserAsync(Stream stream, CancellationToken ct = default)
	{
		_logger.LogDebug("Started unpacking replay.");

		ProcessStartInfo startInfo = new()
		{
			UseShellExecute = false,
			RedirectStandardInput = true,
			RedirectStandardOutput = true,
			FileName = _configuration["PythonUnpacker:PythonPath"],
			Arguments = _configuration["PythonUnpacker:UnpackerPath"],
		};
		
		using Process? process = Process.Start(startInfo);

		if (process is not null)
		{
			process.OutputDataReceived += (_, args) => _logger.LogTrace("Data received from process: \n{Data}", args.Data); 
			
			using StreamReader sr = process.StandardOutput;
			Task<string> readTask = sr.ReadToEndAsync();
			
			await using StreamWriter sw = process.StandardInput;
			await stream.CopyToAsync(sw.BaseStream, ct);
			await sw.FlushAsync();
			sw.Close();

			string output = await readTask;
			
			_logger.LogDebug("Finished unpacking replay. Output size: {Size} characters.", output.Length);

			return output;


		}

		return string.Empty;
	}
}