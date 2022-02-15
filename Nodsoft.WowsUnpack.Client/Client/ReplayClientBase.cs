using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Nodsoft.WowsUnpack.Common.Data.DTOs;
using Nodsoft.WowsUnpack.Common.Data.Serialization;

namespace Nodsoft.WowsUnpack.Client.Client;

/// <summary>
/// A simple client implementation to upload a replay file and receive the extracted data.
/// </summary>
public class ReplayClientBase : IReplayClient
{
    private const string UnpackPath = "/api/v1/Replay";
    private readonly HttpClient httpClient;
    private readonly string unpackHost;
    protected const string DefaultUnpackHost = "https://wows-unpack.nodsoft.net";

    /// <summary>
    /// Creates a new instance of the <see cref="ReplayClientBase"/>.
    /// </summary>
    /// <param name="httpClient">The http client to use for the request. For ideal performance, the client should support compression.</param>
    /// <param name="unpackHost">The custom host to use for the unpacking.</param>
    public ReplayClientBase(HttpClient httpClient, string unpackHost = DefaultUnpackHost)
    {
        this.httpClient = httpClient;
        this.unpackHost = unpackHost;
    }

    public async Task<JsonReplayDto?> PostReplayDtoAsync(Stream fileContent, string filename)
    {
        MultipartFormDataContent form = new();
        form.Add(new StreamContent(fileContent), "file", filename);

        var response = await httpClient.PostAsync(unpackHost + UnpackPath, form);
        response.EnsureSuccessStatusCode();
        var replayStream = await response.Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<JsonReplayDto>(replayStream, JsonHelper.DeserializationOptions);
    }
}