using System.IO;
using System.Threading.Tasks;
using Nodsoft.WowsUnpack.Client.Client;
using Nodsoft.WowsUnpack.Common.Data.DTOs;
using Xunit;

namespace Nodsoft.WowsUnpack.Client.Tests.DefaultReplayClientTests;

public class DefaultReplayClientTest
{
    [Fact]
    public async Task ProcessReplay()
    {
        const string replayPath = "DefaultReplayClientTests/Yamato-0.11.0.wowsreplay";
        Stream replayStream = File.OpenRead(replayPath);
        DefaultReplayClient replayClient = new();

        JsonReplayDto? dto = await replayClient.PostReplayDtoAsync(replayStream, replayPath);

        Assert.NotNull(dto);
        Assert.Null(dto!.Error);
        Assert.Equal(24, dto.Hidden.Vehicles.Count);
    }
}