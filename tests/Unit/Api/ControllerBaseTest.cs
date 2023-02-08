using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using parcelfy.Application.ParcelTrackers.Abstractions;
using parcelfy.Application.ParcelTrackers.Models;
using parcelfy.Controllers;

namespace Api;

public class ControllerBaseTest
{
	protected readonly Mock<IGetParcelTrackingDetailsById> _getTrackingFromParcelIdMock;
	protected readonly Mock<ICreateParcelTrackingDetailsCommands> _createParcelTrackingDetailsCommandsMock;
	protected readonly Mock<ILogger<ParcelTrackerController>> _loggerMock;
	protected readonly Mock<IValidator<ParcelTrackerHistory>> _validatorMock;

	protected ParcelTrackerController _parcelTrackerController;

	public ControllerBaseTest()
	{
		_getTrackingFromParcelIdMock = new Mock<IGetParcelTrackingDetailsById>();
		_createParcelTrackingDetailsCommandsMock = new Mock<ICreateParcelTrackingDetailsCommands>();
		_loggerMock = new Mock<ILogger<ParcelTrackerController>>();
		_validatorMock = new Mock<IValidator<ParcelTrackerHistory>>();

		_parcelTrackerController = new ParcelTrackerController(
			_getTrackingFromParcelIdMock.Object,
			_createParcelTrackingDetailsCommandsMock.Object,
			_loggerMock.Object,
			_validatorMock.Object
			);		
	}
}
