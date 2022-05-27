using System.IO;
using System.Text.Json;
using Nodsoft.WowsUnpack.Common.Data.DTOs;
using Nodsoft.WowsUnpack.Common.Data.Serialization;
using Xunit;

namespace Nodsoft.WowsUnpack.Common.Tests.DeserializationTests;

public class DeserializationTest
{
    [Fact]
    public void DeserializeSampleData_0110()
    {
        const string testfile = "DeserializationTests/sampleData_0.11.0.json";

        string fileContent = File.ReadAllText(testfile);
        JsonReplayDto? replayDto = JsonSerializer.Deserialize<JsonReplayDto>(fileContent, JsonHelper.DeserializationOptions);

        Assert.NotNull(replayDto);
        
        Assert.All(replayDto!.Hidden.Vehicles, entry => Assert.Equal(6, entry.Value.Client.CrewModifiersCompactParams.LearnedSkills.Length));

        Assert.True(replayDto.Hidden.PlayerId > 0);
        Assert.True(replayDto.Hidden.BattleResult.WinnerTeamId > -1);
    }
}