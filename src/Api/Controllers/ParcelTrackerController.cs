namespace parcelfy.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class ParcelTrackerController : ControllerBase
{
	private readonly IGetParcelTrackingDetailsById _getTrackingFromParcelId;
	private readonly ILogger<ParcelTrackerController> _logger;
	public ParcelTrackerController(IGetParcelTrackingDetailsById getTrackingFromParcelId, ILogger<ParcelTrackerController> logger)
	{
		_getTrackingFromParcelId = getTrackingFromParcelId;
		_logger = logger;
	}

	/// <summary>
	/// Get tracking details for a specific parcel Id
	/// </summary>
	/// <param name="parcelId"></param>
	/// <returns>Tracking details</returns>
	[HttpGet("{parcelId}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<ParcelTracker>> ParcelToTrack(string parcelId)
	{
		try
		{
			if (string.IsNullOrEmpty(parcelId))
			{
				return BadRequest();
			}

			ParcelTracker result = await _getTrackingFromParcelId.GetTrackingDetailsAsync(parcelId).ConfigureAwait(false);

			if (result is null)
			{
				return NotFound();
			}

			return Ok(result);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message, ex);
			return new StatusCodeResult(StatusCodes.Status500InternalServerError);
		}
	}
}
