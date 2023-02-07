namespace parcelfy.Infrastructure.ParcelTrackersRepository.Abstractions;
public interface IInMemoryParcelTrackingRepository
{
	Task<IEnumerable<ParcelTrackerHistoryDto>> GetTrackingDetails(string parcelId);
	void PostTrackingDetailsHistory(ParcelTrackerHistoryDto parcelTrackerHistoryDto);
}
