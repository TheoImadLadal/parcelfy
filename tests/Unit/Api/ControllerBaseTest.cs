using Microsoft.Extensions.Logging;
using Moq;
using parcelfy.Api.Controllers;
using parcelfy.Application.Services;

namespace Api;

public class ControllerBaseTest
{
	protected readonly Mock<IParcelTrackingService> _parcelTrackingServiceMock;
	protected readonly Mock<ILogger<ParcelTrackerController>> _loggerMock;

	protected ParcelTrackerController _parcelTrackerController;

	public ControllerBaseTest()
	{
		_parcelTrackingServiceMock = new Mock<IParcelTrackingService>();
		_loggerMock = new Mock<ILogger<ParcelTrackerController>>();

		_parcelTrackerController = new ParcelTrackerController(
			_parcelTrackingServiceMock.Object,
			_loggerMock.Object
			);
	}
}
