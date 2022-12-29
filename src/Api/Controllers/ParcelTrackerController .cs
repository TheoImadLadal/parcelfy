using Microsoft.AspNetCore.Mvc;
using parcelfy.Application.ParcelTrackers.Abstractions;
using parcelfy.Application.ParcelTrackers.Models;

namespace parcelfy.Controllers;

[ApiController]
[Route("[controller]")]
public class ParcelTrackerController : ControllerBase
{
    private readonly IGetTrackingFromParcelId _getTrackingFromParcelId;
    private readonly ILogger<ParcelTrackerController> _logger;

    public ParcelTrackerController(IGetTrackingFromParcelId getTrackingFromParcelId, ILogger<ParcelTrackerController> logger)
    {
        _getTrackingFromParcelId = getTrackingFromParcelId ?? throw new ArgumentNullException(nameof(getTrackingFromParcelId));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    [HttpGet("{parcelId}")] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ParcelTracker?>> ParcelToTrack(string parcelId)
    {
            if (string.IsNullOrEmpty(parcelId))
            {
                return BadRequest();
            }
            
            ParcelTracker? result = await _getTrackingFromParcelId.GetTrackingDetails(parcelId).ConfigureAwait(false);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);       
    }
}
