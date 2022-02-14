using System;
using System.Collections.Generic;

namespace Nodsoft.WowsUnpack.Common.Data.DTOs;

public sealed record HiddenVehicleDto(
    ClientPropertiesDto Client,
    Dictionary<string, object> Cell,
    Dictionary<string, object> Base);

public sealed record ClientPropertiesDto
{
    public int HasAirTargetsInRange { get; init; }

    public int IsAntiAirMode { get; init; }

    public int BatterySurfacingTime { get; init; }

    public int BuoyancyCurrentState { get; init; }

    public int BuoyancyRudderIndex { get; init; }

    public int IsOnForsage { get; init; }

    public int TeamId { get; init; }

    public int UiEnabled { get; init; }

    public int IsAlive { get; init; }

    public int SpeedSignDir { get; init; }

    public int EnginePower { get; init; }

    public int EngineDir { get; init; }

    public int IgnoreMapBorders { get; init; }

    public int IsBot { get; init; }

    public int IsFogHornOn { get; init; }

    public int BlockedControls { get; init; }

    public int IsInvisible { get; init; }

    public int HasActiveMainSquadron { get; init; }

    public int IsInRageMode { get; init; }

    public int CanUseDc { get; init; }

    public int BurningFlags { get; init; }

    public int TargetLocalPos { get; init; }

    public int TorpedoLocalPos { get; init; }

    public int LaserTargetLocalPos { get; init; }

    public int WaveLocalPos { get; init; }

    public int WeaponLockFlags { get; init; }

    public int ServerSpeedRaw { get; init; }

    public int RespawnTime { get; init; }

    public float AirDefenseDispRadius { get; init; }

    public float Health { get; init; }

    public float RegenerationHealth { get; init; }

    public float RegeneratedHealth { get; init; }

    public float MaxHealth { get; init; }

    public float Energy { get; init; }

    public float BuoyancyCurrentWaterline { get; init; }

    public float RegenCrewHpLimit { get; init; }

    public float Buoyancy { get; init; }

    public int VisibilityFlags { get; init; }

    public int Owner { get; init; }

    public int SelectedWeapon { get; init; }

    public int MaxServerSpeedRaw { get; init; }

    public float Draught { get; init; }

    public float RuddersAngle { get; init; }

    public float DeepRuddersAngle { get; init; }

    public int[] AtbaTargets { get; init; } = Array.Empty<int>();

    public object[][] AirDefenseTargetIds { get; init; } = Array.Empty<object[]>();

    public AntiAirAuraDto[] AntiAirAuras { get; init; } = Array.Empty<AntiAirAuraDto>();

    public object[] HeatInfos { get; init; } = Array.Empty<object>();

    public object[] Effects { get; init; } = Array.Empty<object>();

    public object[] Sounds { get; init; } = Array.Empty<object>();

    public string ShipConfig { get; init; } = string.Empty;

    public CrewParamsDto CrewModifiersCompactParams { get; init; } = new(0, 0, Array.Empty<int[]>());

    public object[] DebugText { get; init; } = Array.Empty<object>();

    public object[] MiscsPresetsStatus { get; init; } = Array.Empty<object>();

    public string TriggeredSkillsData { get; init; } = string.Empty;

    public string DeathSettings { get; init; } = string.Empty;

    public object State { get; init; } = new();
}

public sealed record CrewParamsDto(long ParamsId, int IsInAdaptation, int[][] LearnedSkills);

public sealed record AntiAirAuraDto(int Id, int Enabled);