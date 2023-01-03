namespace parcelfy.Infrastructure.ParcelTrackersRepository.Abstractions;
public interface IHttpParcelTrackingRepository
{
    Task<ParcelTrackerDto?> GetTrackingDetails(string parcelId);
}
