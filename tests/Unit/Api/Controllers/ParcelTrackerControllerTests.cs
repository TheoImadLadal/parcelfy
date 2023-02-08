using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using parcelfy.Application.ParcelTrackers.Models;
using Xunit;

namespace Api.Controllers;

public class ParcelTrackerControllerTests : ControllerBaseTest
{
	protected static readonly ParcelTracker ExpectedParcelTracker = new()
	{
		IdShip = "LU680211095FR",
		ReturnCode = 200,
		ReturnMessage = "Votre suivi",
		Shipment = new ParcelTracker.ShipmentDomain
		{
			Product = "Courrier international",
			Url = "https://www.laposte.fr/outils/suivre-vos-envois?code=LU680211095FR",
			IdShip = "LU680211095FR",
			IsFinal = true,
			Event = new List<ParcelTracker.Event>
			{
				new ParcelTracker.Event
				{
					Code = "DR1",
					Date = System.DateTime.Now,
					Label = "La Poste est prête à prendre en charge votre envoi. Dès qu’il nous sera confié, vous pourrez suivre son trajet ici."
				}
			}
		}
	};

	protected static readonly ParcelTrackerHistory ExpectedParcelTrackerhistory = new()
	{
		ParcelId = "LU680211095FR",
		EventCode = "DR1",
		EventMessage = "La Poste est prête à prendre en charge votre envoi.Dès qu’il nous sera confié,",
		EventDate = DateTime.Now,
		Product	= "Courrier international",
		IsFinal	= false,
		URL = "https://www.laposte.fr/outils/suivre-vos-envois?code=LU680211095FR"
	};

	#region GET
	[Fact]
	public async Task ParcelToTrack_Returns200Ok_WhenParcelIdIsValid()
	{
		// Arrange
		var parcelId = "LU680211095FR";
		_getTrackingFromParcelIdMock.
			Setup(x => x.GetTrackingDetailsAsync(parcelId)).
			ReturnsAsync(ExpectedParcelTracker);

		// Act
		var httpResponse = await _parcelTrackerController.ParcelToTrack(parcelId).ConfigureAwait(false);

		// Assert
		httpResponse
			.Result
			.Should()
			.BeOfType<OkObjectResult>()
			.Which
			.Value
			.Should()
			.BeOfType<ParcelTracker>()
			.And
			.Be(ExpectedParcelTracker);
	}

	[Fact]
	public async Task ParcelToTrack_Returns400BadRequest_WhenParcelIdIsEmpty()
	{
		// Arrange
		var parcelId = "";
		_getTrackingFromParcelIdMock.
			Setup(x => x.GetTrackingDetailsAsync(parcelId)).
			ReturnsAsync(ExpectedParcelTracker);

		// Act
		var httpResponse = await _parcelTrackerController.ParcelToTrack(parcelId).ConfigureAwait(false);

		// Assert
		httpResponse
			.Result
			.Should()
			.BeOfType<BadRequestResult>();
	}

	[Fact]
	public async Task ParcelToTrack_Returns404NotFound_WhenResultIsNull()
	{
		// Arrange
		ParcelTracker? parceltracker = null; 
		var parcelId = "LU680211095FR";
		_getTrackingFromParcelIdMock.
			Setup(x => x.GetTrackingDetailsAsync(parcelId)).
			ReturnsAsync(parceltracker);

		// Act
		var httpResponse = await _parcelTrackerController.ParcelToTrack(parcelId).ConfigureAwait(false);

		// Assert
		httpResponse
			.Result
			.Should()
			.BeOfType<NotFoundResult>();
	}

	[Fact]
	public async Task ParcelToTrack_Returns500InternalServerError_WhenExceptionOccurs()
	{
		// Arrange
		var parcelId = "LU680211095FR";
		_getTrackingFromParcelIdMock.
			Setup(x => x.GetTrackingDetailsAsync(parcelId)).
			Throws<Exception>();

		// Act
		var httpResponse = await _parcelTrackerController.ParcelToTrack(parcelId).ConfigureAwait(false);

		// Assert
		var statusCodeResult = httpResponse.Result as StatusCodeResult;
		httpResponse
			.Result
			.Should()
			.BeOfType<StatusCodeResult>();
		statusCodeResult?.
			StatusCode.
			Should().
			Be(StatusCodes.Status500InternalServerError);

		_loggerMock.Verify(x => x.Log(LogLevel.Error,
								It.IsAny<EventId>(),
								It.IsAny<It.IsAnyType>(),
								It.IsAny<Exception>(),
								(Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Once);
	}
	#endregion GET

	#region POST
	[Fact]
	public async Task ParcelToTrack_ValidModel_ShouldReturnCreatedResult()
	{
		// Arrange
		_validatorMock.
			Setup(x => x.ValidateAsync(ExpectedParcelTrackerhistory, default)).
			ReturnsAsync(new ValidationResult());
		_createParcelTrackingDetailsCommandsMock.
			Setup(x => x.CreateTrackingDetailsHistoryAsync(ExpectedParcelTrackerhistory)).
			Returns(Task.CompletedTask);

		// Act
		var httpResponse = await _parcelTrackerController.ParcelToTrack(ExpectedParcelTrackerhistory).ConfigureAwait(false);

		// Assert
		var statusCodeResult = httpResponse.Result as StatusCodeResult;
		httpResponse
			.Result
			.Should()
			.BeOfType<ObjectResult>()
			.Which
			.Value
			.Should()
			.BeOfType<ParcelTrackerHistory>()
			.And
			.Be(ExpectedParcelTrackerhistory);
		statusCodeResult?
			.StatusCode
			.Should()
			.Be((int)HttpStatusCode.Created);
	}

	[Fact]
	public async Task ParcelToTrack_InvalidModel_ShouldReturnBadRequestResult()
	{
		// Arrange
		var validationResult = new ValidationResult(new[] { new ValidationFailure("", "") });
		_validatorMock.
			Setup(x => x.ValidateAsync(ExpectedParcelTrackerhistory, default)).
			ReturnsAsync(validationResult);

		// Act
		var httpResponse = await _parcelTrackerController.ParcelToTrack(ExpectedParcelTrackerhistory).ConfigureAwait(false);

		// Assert
		var statusCodeResult = httpResponse.Result as StatusCodeResult;
		httpResponse.Result.Should().BeOfType<BadRequestObjectResult>();
		statusCodeResult?
			.StatusCode
			.Should()
			.Be((int)HttpStatusCode.BadRequest);

	}

	[Fact]
	public async Task ParcelToTrack_InternalServerError_ShouldReturn500()
	{
		// Arrange
		_createParcelTrackingDetailsCommandsMock.
			Setup(x => x.CreateTrackingDetailsHistoryAsync(ExpectedParcelTrackerhistory)).
			Throws<Exception>();

		// Act
		var httpResponse = await _parcelTrackerController.ParcelToTrack(ExpectedParcelTrackerhistory).ConfigureAwait(false);

		// Assert
		var statusCodeResult = httpResponse.Result as StatusCodeResult;
		httpResponse
			.Result
			.Should()
			.BeOfType<StatusCodeResult>();
		statusCodeResult?.
			StatusCode.
			Should().
			Be(StatusCodes.Status500InternalServerError);

		_loggerMock.Verify(x => x.Log(LogLevel.Error,
								It.IsAny<EventId>(),
								It.IsAny<It.IsAnyType>(),
								It.IsAny<Exception>(),
								(Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Once);
	}
	#endregion POST 

}


