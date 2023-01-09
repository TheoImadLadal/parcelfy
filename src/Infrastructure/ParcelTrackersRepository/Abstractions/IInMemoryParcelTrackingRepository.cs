namespace parcelfy.Infrastructure.ParcelTrackersRepository.Abstractions;
public interface IInMemoryParcelTrackingRepository
{
	public IEnumerable<ParcelTrackHistory> GetTrackingDetails(string parcelId);
}
