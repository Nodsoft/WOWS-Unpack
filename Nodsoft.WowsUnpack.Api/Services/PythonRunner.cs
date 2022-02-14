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

	public async Task<string> RunScriptAsync(string args)
	{
		/*
		 * TODO: Get OS-specific shell executable
		 */
		string shellExec = string.Empty;

		//shellExec = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? @"C:\Windows\System32\WindowsPowershell\v1.0\powershell.exe" : @"/bin/bash";
		shellExec = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? @"C:\Python310\python.exe" : throw new NotImplementedException();

//		if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
//		{
//			args = "/C " + args;
//		}
		
		ProcessStartInfo startInfo = new()
		{
			UseShellExecute = false,
			RedirectStandardOutput = true,
			FileName = shellExec,
			Arguments = args,
			WorkingDirectory = @"D:\Code\Source\WOWS-Unpack\replays_unpack"
		};
		
		using Process? process = Process.Start(startInfo);

		if (process is not null)
		{
			using StreamReader reader = process.StandardOutput;
			return await reader.ReadToEndAsync();
		}

		return string.Empty;
	}
}