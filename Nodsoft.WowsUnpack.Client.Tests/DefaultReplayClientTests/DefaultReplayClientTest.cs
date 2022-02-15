using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Nodsoft.WowsUnpack.Client.Client;
using NUnit.Framework;

namespace Nodsoft.WowsUnpack.Client.Tests.DefaultReplayClientTests;

[TestFixture]
public class DefaultReplayClientTest
{
    [Test]
    public async Task ProcessReplay()
    {
        const string replayPath = "DefaultReplayClientTests/Yamato-0.11.0.wowsreplay";
        Stream replayStream = File.OpenRead(replayPath);
        DefaultReplayClient replayClient = new();

        var dto = await replayClient.PostReplayDtoAsync(replayStream, replayPath);

        dto.Should().NotBeNull();
        dto!.Error.Should().BeNull();
        dto.Hidden.Vehicles.Should().HaveCount(24);
    }
}