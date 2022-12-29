using parcelfy.Infrastructure.ParcelTrackersRepository.Dtos;

namespace parcelfy.Infrastructure.ParcelTrackersRepository.Abstractions;
public interface IParcelTrackingRepository
{
    Task<ParcelTrackerDTO?> GetTrackingDetails(string parcelId);
}
