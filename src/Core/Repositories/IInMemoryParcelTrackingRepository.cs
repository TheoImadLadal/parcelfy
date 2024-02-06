using parcelfy.Core.Entities;

namespace parcelfy.Core.Repositories;
public interface IInMemoryParcelTrackingRepository
{
	Task<IEnumerable<ParcelTrackerHistoryEntity>> GetTrackingDetails(string parcelId);
	void PostTrackingDetailsHistory(ParcelTrackerHistoryEntity parcelTrackerHistoryDto);
}
