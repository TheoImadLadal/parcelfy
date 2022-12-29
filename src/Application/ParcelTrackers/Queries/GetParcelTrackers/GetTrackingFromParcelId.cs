using Microsoft.Extensions.Logging;
using parcelfy.Application.ParcelTrackers.Abstractions;
using parcelfy.Application.ParcelTrackers.Mappers;
using parcelfy.Application.ParcelTrackers.Models;
using parcelfy.Infrastructure.ParcelTrackersRepository.Abstractions;
using parcelfy.Infrastructure.ParcelTrackersRepository.Dtos;

namespace parcelfy.Application.ParcelTrackers.Queries.GetTrackingFromParcelId;

public class GetTrackingFromParcelId : IGetTrackingFromParcelId
{

    private readonly ILogger<GetTrackingFromParcelId> _logger;
    private readonly IParcelTrackingRepository _parcelTrackingRepository;

    public GetTrackingFromParcelId(ILogger<GetTrackingFromParcelId>? logger, IParcelTrackingRepository parcelTrackingRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _parcelTrackingRepository = parcelTrackingRepository ?? throw new ArgumentNullException(nameof(parcelTrackingRepository));
    }

    public async Task<ParcelTracker?> GetTrackingDetails(string parcelId)
    {
        ParcelTracker? result = null;
        
        try
        {
            ParcelTrackerDTO? parcelTrackerDto = await _parcelTrackingRepository.GetTrackingDetails(parcelId).ConfigureAwait(false);
            if (parcelTrackerDto != null)
            {
                result = ParcelTrackerMapping.FromDTOToDomain(parcelTrackerDto);
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
