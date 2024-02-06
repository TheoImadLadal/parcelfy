using parcelfy.Core.Entities;

namespace parcelfy.Core.Repositories;
public interface IHttpParcelTrackingRepository
{
	Task<ParcelTrackerEntity> GetTrackingDetails(string parcelId);
}
