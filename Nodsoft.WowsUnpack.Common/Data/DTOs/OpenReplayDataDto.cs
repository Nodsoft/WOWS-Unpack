namespace Nodsoft.WowsUnpack.Common.Data.DTOs;

public sealed record OpenReplayDataDto
{
    public string MatchGroup { get; init; } = string.Empty;

    public long GameMode { get; init; }

    public string ClientVersionFromExe { get; init; } = string.Empty;

    public long ScenarioUiCategoryId { get; init; }

    public string MapDisplayName { get; init; } = string.Empty;

    public long MapId { get; init; }

    public string ClientVersionFromXml { get; init; } = string.Empty;

    public Dictionary<string, string[]> WeatherParams { get; init; } = new();

    public object[] DisabledShipClasses { get; init; } = Array.Empty<object>();

    public long PlayersPerTeam { get; init; }

    public long Duration { get; init; }

    public string GameLogic { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public string Scenario { get; init; } = string.Empty;

    public long PlayerId { get; init; }

    public List<OpenVehicleDto> Vehicles { get; init; } = new();

    public string GameType { get; init; } = string.Empty;

    public string DateTime { get; init; } = string.Empty;

    public string MapName { get; init; } = string.Empty;

    public string PlayerName { get; init; } = string.Empty;

    public long ScenarioConfigId { get; init; }

    public long TeamsCount { get; init; }

    public string Logic { get; init; } = string.Empty;

    public string PlayerVehicle { get; init; } = string.Empty;

    public long BattleDuration { get; init; }

    public object MapBorder { get; init; } = 0;
}

public sealed record OpenVehicleDto
{
    public long ShipId { get; init; }

    public long Relation { get; init; }

    public long Id { get; init; }

    public string Name { get; init; } = string.Empty;
}