namespace parcelfy.Application.Services;

public interface IParcelTrackingService
{
	Task<ParcelTrackerDTO> GetTrackingDetailsById(string parcelId);

	Task CreateTrackingDetails(ParcelTrackerHistoryDTO parcelTrackerHistory);
}
