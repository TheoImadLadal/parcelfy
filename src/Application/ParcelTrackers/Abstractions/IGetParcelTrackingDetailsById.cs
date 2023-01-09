namespace parcelfy.Application.ParcelTrackers.Abstractions;

public interface IGetParcelTrackingDetailsById
{
	Task<ParcelTracker> GetTrackingDetailsAsync(string parcelId);
}
