using System.IO;
using System.Threading.Tasks;
using Nodsoft.WowsUnpack.Common.Data.DTOs;

namespace Nodsoft.WowsUnpack.Client.Client;

/// <summary>
/// A simple client to upload a replay file and receive the extracted data.
/// </summary>
public interface IReplayClient
{
    /// <summary>
    /// Uploads a replay file to the api and returns the processed data.
    /// </summary>
    /// <param name="fileContent">A stream containing the replay file content.</param>
    /// <returns>A <see cref="JsonReplayDto"/> containing the information extracted from the replay.</returns>
    Task<JsonReplayDto?> PostReplayDtoAsync(Stream fileContent, string filename);
}