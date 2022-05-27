using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;

namespace Nodsoft.WowsUnpack.Web.Sanitizer.Models;

public record ReplayUploadForm
{
	[Required]
	public IBrowserFile? File { get; set; }
}