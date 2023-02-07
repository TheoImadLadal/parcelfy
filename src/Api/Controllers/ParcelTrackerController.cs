namespace parcelfy.Controllers;

[ApiController]
[Route("parcel-tracker")]
[Produces("application/json")]
public class ParcelTrackerController : ControllerBase
{
	private readonly IGetParcelTrackingDetailsById _getTrackingFromParcelId;
	private readonly ICreateParcelTrackingDetailsCommands _createParcelTrackingDetailsCommands;
	private readonly ILogger<ParcelTrackerController> _logger;
	private readonly IValidator<ParcelTrackerHistory> _validator;

	public ParcelTrackerController(IGetParcelTrackingDetailsById getTrackingFromParcelId, 
		ICreateParcelTrackingDetailsCommands createParcelTrackingDetailsCommands, 
		ILogger<ParcelTrackerController> logger,
		IValidator<ParcelTrackerHistory> validator)
	{
		_getTrackingFromParcelId = getTrackingFromParcelId;
		_createParcelTrackingDetailsCommands = createParcelTrackingDetailsCommands;
		_logger = logger;
		_validator = validator;
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

	/// <summary>
	/// Post history tracking details
	/// </summary>
	/// <param name="parcelTrackerHistory"></param>
	/// <returns> Summary of the inserted element</returns>
	[HttpPost()]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	[DisableRequestSizeLimit]
	public async Task<ActionResult<ParcelTrackerHistory>> ParcelToTrack(ParcelTrackerHistory parcelTrackerHistory)
	{
		try
		{
			ValidationResult validationResult = await _validator.ValidateAsync(parcelTrackerHistory).ConfigureAwait(false);
			if (!validationResult.IsValid)
			{
				var errorsMessages = Results.ValidationProblem(validationResult.ToDictionary());
				return BadRequest(errorsMessages);
			}

			await _createParcelTrackingDetailsCommands.CreateTrackingDetailsHistoryAsync(parcelTrackerHistory).ConfigureAwait(false);

			return StatusCode(StatusCodes.Status201Created, parcelTrackerHistory);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message, ex);
			return new StatusCodeResult(StatusCodes.Status500InternalServerError);
		}
	}

}
