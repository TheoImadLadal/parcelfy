using parcelfy.Application.UseCases;

namespace parcelfy.Application.Services;

public class ParcelTrackingService(IGetParcelTrackingDetails getParcelTrackingDetails, ICreateParcelTrackingDetails createParcelTrackingDetails) : IParcelTrackingService
{

	private readonly IGetParcelTrackingDetails _getParcelTrackingDetails = getParcelTrackingDetails;
	private readonly ICreateParcelTrackingDetails _createParcelTrackingDetails = createParcelTrackingDetails;

	public async Task<ParcelTrackerDTO> GetTrackingDetailsById(string parcelId)
	{
		return await _getParcelTrackingDetails.GetTrackingDetailsAsync(parcelId);
	}

	public async Task CreateTrackingDetails(ParcelTrackerHistoryDTO parcelTrackerHistory)
	{
		await _createParcelTrackingDetails.CreateTrackingDetailsHistoryAsync(parcelTrackerHistory);
	}
}
