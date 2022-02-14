using System.Text.Json;
using Nodsoft.WowsUnpack.Common.Data.DTOs;

namespace Nodsoft.WowsUnpack.Common.Data.Serialization;

public class DeserializationNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        return name switch
        {
            nameof(HiddenDataDto.BattleResult) => "battle_result",
            nameof(HiddenDataDto.DamageMap) => "damage_map",
            nameof(HiddenDataDto.ShotsDamageMap) => "shots_damage_map",
            nameof(HiddenDataDto.DeathMap) => "death_map",
            nameof(HiddenDataDto.DeathInfo) => "death_info",
            nameof(HiddenDataDto.PlayerId) => "player_id",
            nameof(HiddenDataDto.ControlPoints) => "control_points",
            nameof(HiddenDataDto.ArenaId) => "arena_id",
            nameof(BattleResultDto.WinnerTeamId) => "winner_team_id",
            nameof(BattleResultDto.VictoryType) => "victory_type",
            nameof(DeathInfoEntryDto.KillerId) => "killer_id",
            nameof(ControlPointDto.BuoyModelId) => "buoy_modelID",
            _ => name,
        };
    }
}