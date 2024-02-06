
namespace parcelfy.Application.UseCases;

public interface IGetParcelTrackingDetails
{
	Task<ParcelTrackerDTO> GetTrackingDetailsAsync(string parcelId);
}