using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Nodsoft.WowsUnpack.Api.Services;

/// <summary>
/// POC class to experiment with Python processes
/// </summary>
public class PythonRunner
{
	private readonly ILogger<PythonRunner> _logger;

	public PythonRunner(ILogger<PythonRunner> logger)
	{
		_logger = logger;
	}

	public async Task<string> RunParserAsync(Stream stream, CancellationToken ct = default)
	{
		/*
		 * TODO: Get OS-specific shell executable
		 */
		string shellExec = string.Empty;
		//shellExec = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? @"C:\Windows\System32\WindowsPowershell\v1.0\powershell.exe" : @"/bin/bash";
		shellExec = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? @"C:\Python310\python.exe" : throw new NotImplementedException();

		
		
		ProcessStartInfo startInfo = new()
		{
			UseShellExecute = false,
			RedirectStandardInput = true,
			RedirectStandardOutput = true,
			FileName = shellExec,
			Arguments = @".\replay_parser.py",
			WorkingDirectory = @"D:\Code\Source\WOWS-Unpack\replays_unpack"
		};
		
		using Process? process = Process.Start(startInfo);

		if (process is not null)
		{
			// process.OutputDataReceived += (_, args) => _logger.LogDebug("Data received from process: \n{Data}", args.Data); 
			
			using StreamReader sr = process.StandardOutput;
			Task<string> readTask = sr.ReadToEndAsync();
			
			await using StreamWriter sw = process.StandardInput;
			await stream.CopyToAsync(sw.BaseStream, ct);
			await sw.FlushAsync();
			sw.Close();

			return await readTask;
		}

		return string.Empty;
	}
}