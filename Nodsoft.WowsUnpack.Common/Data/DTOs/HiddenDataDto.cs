namespace Nodsoft.WowsUnpack.Common.Data.DTOs;

public sealed record HiddenDataDto
{
    public object Achievements { get; init; } = new();

    public object Ribbons { get; init; } = new();

    public Dictionary<string, HiddenPlayerDto> Players { get; init; } = new();

    public BattleResultDto BattleResult { get; init; } = new(-1, -1);

    public Dictionary<string, Dictionary<string, float[]>> DamageMap { get; init; } = new();

    public Dictionary<string, Dictionary<string, float>> ShotsDamageMap { get; init; } = new();

    public int[][] DeathMap { get; init; } = Array.Empty<int[]>();

    public Dictionary<string, DeathInfoEntryDto> DeathInfo { get; init; } = new();

    public string Map { get; init; } = string.Empty;

    public int PlayerId { get; init; }

    public ControlPointDto[] ControlPoints { get; init; } = Array.Empty<ControlPointDto>();

    public object[] Tasks { get; init; } = Array.Empty<object>();

    public object Skills { get; init; } = new();

    public long ArenaId { get; init; }

    public Dictionary<string, HiddenVehicleDto> Vehicles { get; init; } = new();
}

public sealed record BattleResultDto(int WinnerTeamId, int VictoryType);

public sealed record DeathInfoEntryDto(int KillerId, string Icon, string Name);

public sealed record ControlPointDto
{
    public float[] Position { get; init; } = Array.Empty<float>();

    public float Radius { get; init; }

    public float InnerRadius { get; init; }

    public long BuoyModelId { get; init; }

    public int NextControlPoint { get; init; }

    public int ControlPointType { get; init; }

    public string TimerName { get; init; } = string.Empty;

    public int TeamId { get; init; }

    public float Progress { get; init; }

    public float NeutralProgress { get; init; }

    public int InvaderTeam { get; init; }

    public int BothInside { get; init; }

    public int HasInvaders { get; init; }

    public int IsEnabled { get; init; }

    public int IsVisible { get; init; }

    public float CaptureTime { get; init; }

    public float CaptureSpeed { get; init; }
}