using System.Diagnostics.CodeAnalysis;

namespace parcelfy.Core.Entities;

[ExcludeFromCodeCoverage]
public class ParcelTrackerHistoryEntity
{
	public string parcelid { get; set; }

	public string eventcode { get; set; }

	public string eventmessage { get; set; }

	public DateTime eventdate { get; set; }

	public string product { get; set; }

	public bool isfinal { get; set; }

	public string url { get; set; }
}