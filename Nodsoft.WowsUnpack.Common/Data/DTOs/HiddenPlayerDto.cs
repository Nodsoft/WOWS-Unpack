namespace Nodsoft.WowsUnpack.Common.Data.DTOs;

public sealed record HiddenPlayerDto
{
    public int Id { get; init; }

    public bool IsConnected { get; init; }

    public int DbId { get; init; }

    public bool AntiAbuseEnabled { get; init; }

    public int AvatarId { get; init; }

    public long[] CamouflageInfo { get; init; } = Array.Empty<long>();

    public int ClanColor { get; init; }

    public int ClanId { get; init; }

    public string ClanTag { get; init; } = string.Empty;

    public object[] CrewParams { get; init; } = Array.Empty<object>();

    public long[] DogTag { get; init; } = Array.Empty<long>();

    public int FragsCount { get; init; }

    public bool FriendlyFireEnabled { get; init; }

    public bool InvitationsEnabled { get; init; }

    public bool IsAbuser { get; init; }

    public bool IsAlive { get; init; }

    public bool IsBot { get; init; }

    public bool IsClientLoaded { get; init; }

    public bool IsHidden { get; init; }

    public bool IsLeaver { get; init; }

    public bool IsPreBattleOwner { get; init; }

    public bool IsTShooter { get; init; }

    public int KilledBuildingsCount { get; init; }

    public int MaxHealth { get; init; }

    public string Name { get; init; } = string.Empty;

    public PlayermodeDto PlayerMode { get; init; } = new(-1, -1);

    public int PreBattleIdOnStart { get; init; }

    public int PreBattleSign { get; init; }

    public int PrebattleId { get; init; }

    public string Realm { get; init; } = string.Empty;

    public Dictionary<string, string> ShipComponents { get; init; } = new();

    public string ShipConfigDump { get; init; } = string.Empty;

    public int ShipId { get; init; }

    public long ShipParamsId { get; init; }

    public long SkinId { get; init; }

    public int TeamId { get; init; }

    public bool TtkStatus { get; init; }
}

public sealed record PlayermodeDto(int PlayerModeType, int ObservedTeamId);