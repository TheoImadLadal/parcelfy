using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using parcelfy.Core.DTOs;
using Xunit;

namespace Api.Controllers;

public class ParcelTrackerControllerTests : ControllerBaseTest
{
	protected static readonly ParcelTrackerDTO ExpectedParcelTracker = new()
	{
		IdShip = "LU680211095FR",
		ReturnCode = 200,
		ReturnMessage = "Votre suivi",
		Shipment = new ParcelTrackerDTO.ShipmentDomain
		{
			Product = "Courrier international",
			Url = "https://www.laposte.fr/outils/suivre-vos-envois?code=LU680211095FR",
			IdShip = "LU680211095FR",
			IsFinal = true,
			Event = new List<ParcelTrackerDTO.Event>
			{
				new ParcelTrackerDTO.Event
				{
					Code = "DR1",
					Date = System.DateTime.Now,
					Label = "La Poste est prête à prendre en charge votre envoi. Dès qu’il nous sera confié, vous pourrez suivre son trajet ici."
				}
			}
		}
	};

	protected static readonly ParcelTrackerHistoryDTO ExpectedParcelTrackerhistory = new()
	{
		ParcelId = "LU680211095FR",
		EventCode = "DR1",
		EventMessage = "La Poste est prête à prendre en charge votre envoi.Dès qu’il nous sera confié,",
		EventDate = DateTime.Now,
		Product = "Courrier international",
		IsFinal = false,
		URL = "https://www.laposte.fr/outils/suivre-vos-envois?code=LU680211095FR"
	};

	#region GET
	[Fact]
	public async Task ParcelToTrack_Returns200Ok_WhenParcelIdIsValid()
	{
		// Arrange
		var parcelId = "LU680211095FR";
		_parcelTrackingServiceMock.
			Setup(x => x.GetTrackingDetailsById(parcelId)).
			ReturnsAsync(ExpectedParcelTracker);

		// Act
		var httpResponse = await _parcelTrackerController.ParcelToTrack(parcelId);

		// Assert
		httpResponse
			.Result
			.Should()
			.BeOfType<OkObjectResult>()
			.Which
			.Value
			.Should()
			.BeOfType<ParcelTrackerDTO>()
			.And
			.Be(ExpectedParcelTracker);
	}

	[Fact]
	public async Task ParcelToTrack_Returns400BadRequest_WhenParcelIdIsEmpty()
	{
		// Arrange
		var parcelId = "";
		_parcelTrackingServiceMock.
			Setup(x => x.GetTrackingDetailsById(parcelId)).
			ReturnsAsync(ExpectedParcelTracker);

		// Act
		var httpResponse = await _parcelTrackerController.ParcelToTrack(parcelId);

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
		ParcelTrackerDTO? parceltracker = null;
		var parcelId = "LU680211095FR";
		_parcelTrackingServiceMock.
			Setup(x => x.GetTrackingDetailsById(parcelId)).
			ReturnsAsync(parceltracker);

		// Act
		var httpResponse = await _parcelTrackerController.ParcelToTrack(parcelId);

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
		_parcelTrackingServiceMock.
			Setup(x => x.GetTrackingDetailsById(parcelId)).
			Throws<Exception>();

		// Act
		var httpResponse = await _parcelTrackerController.ParcelToTrack(parcelId);

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

#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
		_loggerMock.Verify(x => x.Log(LogLevel.Error,
								It.IsAny<EventId>(),
								It.IsAny<It.IsAnyType>(),
								It.IsAny<Exception>(),
								(Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Once);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
	}
	#endregion GET

	#region POST
	[Fact]
	public async Task ParcelToTrack_ValidModel_ShouldReturnCreatedResult()
	{
		// Arrange
		_parcelTrackingServiceMock.
			Setup(x => x.CreateTrackingDetails(ExpectedParcelTrackerhistory)).
			Returns(Task.CompletedTask);

		// Act
		var httpResponse = await _parcelTrackerController.ParcelToTrack(ExpectedParcelTrackerhistory);

		// Assert
		var statusCodeResult = httpResponse.Result as StatusCodeResult;
		httpResponse
			.Result
			.Should()
			.BeOfType<ObjectResult>()
			.Which
			.Value
			.Should()
			.BeOfType<ParcelTrackerHistoryDTO>()
			.And
			.Be(ExpectedParcelTrackerhistory);
		statusCodeResult?
			.StatusCode
			.Should()
			.Be((int)HttpStatusCode.Created);
	}

	[Fact]
	public async Task ParcelToTrack_InternalServerError_ShouldReturn500()
	{
		// Arrange
		_parcelTrackingServiceMock.
			Setup(x => x.CreateTrackingDetails(ExpectedParcelTrackerhistory)).
			Throws<Exception>();

		// Act
		var httpResponse = await _parcelTrackerController.ParcelToTrack(ExpectedParcelTrackerhistory);

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

#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
		_loggerMock.Verify(x => x.Log(LogLevel.Error,
								It.IsAny<EventId>(),
								It.IsAny<It.IsAnyType>(),
								It.IsAny<Exception>(),
								(Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Once);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
	}
	#endregion POST 

}


