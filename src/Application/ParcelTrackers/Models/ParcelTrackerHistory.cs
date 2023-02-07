namespace parcelfy.Application.ParcelTrackers.Models;

public class ParcelTrackerHistory
{
	public string ParcelId { get; set; }
	public string EventCode { get; set; }
	public string EventMessage { get; set; }
	public DateTime EventDate { get; set; }
	public string Product { get; set; }
	public bool IsFinal { get; set; }
	public string URL { get; set; }
}

