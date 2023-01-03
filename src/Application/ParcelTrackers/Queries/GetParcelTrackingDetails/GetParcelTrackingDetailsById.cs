using Mapster;

namespace parcelfy.Application.ParcelTrackers.Queries.GetParcelTrackers;

public class GetParcelTrackingDetailsById : IGetParcelTrackingDetailsById
{

    private readonly ILogger<GetParcelTrackingDetailsById> _logger;
    private readonly IHttpParcelTrackingRepository _parcelTrackingRepository;

    public GetParcelTrackingDetailsById(ILogger<GetParcelTrackingDetailsById>? logger, IHttpParcelTrackingRepository parcelTrackingRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _parcelTrackingRepository = parcelTrackingRepository ?? throw new ArgumentNullException(nameof(parcelTrackingRepository));
    }

    public async Task<ParcelTracker?> GetTrackingDetails(string parcelId)
    {
        ParcelTracker? result = null;
        
        try
        {
			ParcelTrackerDto? parcelTrackerDto = await _parcelTrackingRepository.GetTrackingDetails(parcelId).ConfigureAwait(false);
            if (parcelTrackerDto != null)
            {

				ParcelTracker parcelTracker = parcelTrackerDto.Adapt<ParcelTracker>();

				result = parcelTracker;
            }
        }
        catch (Exception ex) when (ex is InvalidOperationException || ex is NullReferenceException || ex is UriFormatException)
        {
            _logger.LogError(ex.Message, ex);
            throw;
        }
        return result;
    }
}
