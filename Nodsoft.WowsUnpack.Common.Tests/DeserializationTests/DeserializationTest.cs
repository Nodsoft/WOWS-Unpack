using System.IO;
using System.Text.Json;
using FluentAssertions;
using Nodsoft.WowsUnpack.Common.Data.DTOs;
using Nodsoft.WowsUnpack.Common.Data.Serialization;
using NUnit.Framework;

namespace Nodsoft.WowsUnpack.Common.Tests.DeserializationTests;

[TestFixture]
public class DeserializationTest
{
    [Test]
    public void DeserializeSampleData_0110()
    {
        const string testfile = "DeserializationTests/sampleData_0.11.0.json";

        string fileContent = File.ReadAllText(testfile);
        JsonReplayDto? replayDto = JsonSerializer.Deserialize<JsonReplayDto>(fileContent, JsonHelper.DeserializationOptions);

        replayDto.Should().NotBeNull();
        replayDto!.Hidden.Vehicles.Should()
            .AllSatisfy(entry => entry.Value.Client.CrewModifiersCompactParams.LearnedSkills.Should().HaveCount(6));
        replayDto.Hidden.PlayerId.Should().BeGreaterThan(0);
        replayDto.Hidden.BattleResult.WinnerTeamId.Should().BeGreaterThan(-1);
    }
}