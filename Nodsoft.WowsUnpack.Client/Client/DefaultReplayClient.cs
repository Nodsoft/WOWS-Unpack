using System.Net;
using System.Net.Http;
using System.Runtime.Versioning;

namespace Nodsoft.WowsUnpack.Client.Client;

/// <summary>
/// A <see cref="IReplayClientBase"/> implementation for all platforms except browser that automatically configures the http client to use gzip or brotli compression.
/// </summary>
[UnsupportedOSPlatform("browser")]
public class DefaultReplayClient : ReplayClientBase
{
    /// <summary>
    /// Creates a new instance of the <see cref="DefaultReplayClient"/> class.
    /// </summary>
    /// <param name="unpackHost">A custom host for the unpacking api.</param>
    public DefaultReplayClient(string unpackHost = DefaultUnpackHost) : base(new(new HttpClientHandler
    {
        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Brotli,
    }), unpackHost)
    {
    }
}