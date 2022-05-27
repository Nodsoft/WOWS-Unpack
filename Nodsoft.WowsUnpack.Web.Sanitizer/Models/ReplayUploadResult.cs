using Nodsoft.WowsReplaysUnpack.Data;

namespace Nodsoft.WowsUnpack.Web.Sanitizer.Models;


public record ReplayUploadResult(ReplayRaw? Replay, Exception? ParsingException);