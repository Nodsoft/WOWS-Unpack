namespace Nodsoft.WowsUnpack.Common.Data.DTOs;

public record JsonReplayDto(OpenReplayDataDto Open, object[] ExtraData, HiddenDataDto Hidden, object Error);