using parcelfy.Application.ParcelTrackers.Models;

namespace parcelfy.Application.ParcelTrackers.Abstractions;

public interface IGetTrackingFromParcelId
{ 
    Task<ParcelTracker?> GetTrackingDetails(string parcelId);
}
