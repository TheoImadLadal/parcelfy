using webUI.Components.Models;

namespace webUI.Components.Data;

public interface IParcelTrackerApiService
{
	Task<ParcelTracker?> GetParcelTrackingDetails(string parcelId);
}
