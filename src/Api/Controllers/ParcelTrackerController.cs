using parcelfy.Application.Services;

namespace parcelfy.Api.Controllers;

[ApiController]
[Route("parcel-tracker")]
[Produces("application/json")]
public class ParcelTrackerController(IParcelTrackingService parcelTrackingService,
	ILogger<ParcelTrackerController> logger
	) : ControllerBase
{
	private readonly IParcelTrackingService _parcelTrackingService = parcelTrackingService;
	private readonly ILogger<ParcelTrackerController> _logger = logger;

	/// <summary>
	/// Get tracking details for a specific parcel Id
	/// </summary>
	/// <param name="parcelId"></param>
	/// <returns>Tracking details</returns>
	[HttpGet("{parcelId}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<ParcelTrackerDTO>> ParcelToTrack(string parcelId)
	{
		try
		{
			if (string.IsNullOrEmpty(parcelId))
			{
				return BadRequest();
			}

			ParcelTrackerDTO result = await _parcelTrackingService.GetTrackingDetailsById(parcelId);

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

	/// <summary>
	/// Post history tracking details
	/// </summary>
	/// <param name="parcelTrackerHistory"></param>
	/// <returns> Summary of the inserted element</returns>
	[Authorize]
	[HttpPost()]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	[DisableRequestSizeLimit]
	public async Task<ActionResult<ParcelTrackerHistoryDTO>> ParcelToTrack(ParcelTrackerHistoryDTO parcelTrackerHistory)
	{
		try
		{
			await _parcelTrackingService.CreateTrackingDetails(parcelTrackerHistory);

			return StatusCode(StatusCodes.Status201Created, parcelTrackerHistory);
		}
		catch (ValidationException ex)
		{
			_logger.LogError(ex.Message, ex.Errors);
			return BadRequest(new { errors = ex.Errors.Select(e => e.ErrorMessage) });
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message, ex);
			return new StatusCodeResult(StatusCodes.Status500InternalServerError);
		}
	}

}
