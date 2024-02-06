
namespace parcelfy.Application.UseCases;

public interface ICreateParcelTrackingDetails
{
	Task CreateTrackingDetailsHistoryAsync(ParcelTrackerHistoryDTO parcelTrackerHistory);
}