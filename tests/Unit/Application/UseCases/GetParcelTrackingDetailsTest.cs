using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MapsterMapper;
using Moq;
using parcelfy.Application.UseCases;
using parcelfy.Core.DTOs;
using parcelfy.Core.Entities;
using parcelfy.Core.Repositories;
using Xunit;

namespace parcelfy.tests.Application.UseCases;

public class GetParcelTrackingDetailsTest
{
	protected readonly Mock<IMapper> _mapperMock;
	protected readonly Mock<IInMemoryParcelTrackingRepository> _inMemoryParcelTrackersRepositoryMock;
	protected readonly Mock<IHttpParcelTrackingRepository> _iHttpParcelTrackingRepositoryMock;

	protected GetParcelTrackingDetails _getParcelTrackingDetailsById;

	public GetParcelTrackingDetailsTest()
	{
		_mapperMock = new Mock<IMapper>();
		_iHttpParcelTrackingRepositoryMock = new Mock<IHttpParcelTrackingRepository>();
		_inMemoryParcelTrackersRepositoryMock = new Mock<IInMemoryParcelTrackingRepository>();

		_getParcelTrackingDetailsById = new GetParcelTrackingDetails(
			_mapperMock.Object,
			_iHttpParcelTrackingRepositoryMock.Object,
			_inMemoryParcelTrackersRepositoryMock.Object
			);
	}

	protected static readonly List<ParcelTrackerHistoryEntity> ExpectedParcelTrackerHistorydto = new()
	{
		new ParcelTrackerHistoryEntity
		{
			ParcelId = "LU680211095FR",
			EventCode = "DR1",
			EventMessage = "La Poste est prête à prendre en charge votre envoi.Dès qu’il nous sera confié,",
			EventDate = DateTime.Now,
			Product = "Courrier international",
			IsFinal = false,
			URL = "https://www.laposte.fr/outils/suivre-vos-envois?code=LU680211095FR"
		}
	};

	protected static readonly ParcelTrackerEntity ExpectedParcelTrackerdto = new()
	{
		IdShip = "LU680211095FR",
		ReturnCode = 200,
		ReturnMessage = "Votre suivi",
		Shipment = new ParcelTrackerEntity.ShipmentDto
		{
			Product = "Courrier international",
			Url = "https://www.laposte.fr/outils/suivre-vos-envois?code=LU680211095FR",
			IdShip = "LU680211095FR",
			IsFinal = true,
			Event = new List<ParcelTrackerEntity.Event>
			{
				new ParcelTrackerEntity.Event
				{
					Code = "DR1",
					Date = DateTime.Now,
					Label = "La Poste est prête à prendre en charge votre envoi. Dès qu’il nous sera confié, vous pourrez suivre son trajet ici."
				}
			}
		}
	};

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
					Date = DateTime.Now,
					Label = "La Poste est prête à prendre en charge votre envoi. Dès qu’il nous sera confié, vous pourrez suivre son trajet ici."
				}
			}
		}
	};

	[Fact]
	public async Task GetTrackingDetailsAsync_ShouldReturnParcelTracker_WhenDataIsAvailableInInMemoryRepository()
	{
		// Arrange
		var parcelId = "LU680211095FR";
		_inMemoryParcelTrackersRepositoryMock.
			Setup(x => x.GetTrackingDetails(parcelId)).
			ReturnsAsync(ExpectedParcelTrackerHistorydto);
		_mapperMock.
			Setup(x => x.Map<ParcelTrackerDTO>(ExpectedParcelTrackerHistorydto[0])).
			Returns(ExpectedParcelTracker);

		// Act
		var result = await _getParcelTrackingDetailsById.GetTrackingDetailsAsync(parcelId);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(ExpectedParcelTracker, result);
	}

	[Fact]
	public async Task GetTrackingDetailsAsync_ShouldReturnParcelTracker_WhenDataIsNotAvailableInInMemoryRepository()
	{
		// Arrange
		var parcelId = "LU680211095FR";
		var parcelTrackerHistoryDto = new List<ParcelTrackerHistoryEntity>() { };
		_inMemoryParcelTrackersRepositoryMock.
			Setup(x => x.GetTrackingDetails(parcelId)).
			ReturnsAsync(parcelTrackerHistoryDto);
		_iHttpParcelTrackingRepositoryMock.
			Setup(x => x.GetTrackingDetails(parcelId)).
			ReturnsAsync(ExpectedParcelTrackerdto);
		_mapperMock.
			Setup(x => x.Map<ParcelTrackerDTO>(ExpectedParcelTrackerdto)).
			Returns(ExpectedParcelTracker);

		// Act
		var result = await _getParcelTrackingDetailsById.GetTrackingDetailsAsync(parcelId);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(ExpectedParcelTracker, result);
	}

	[Fact]
	public async Task GetTrackingDetailsAsync_ShouldReturnNull_WhenParcelIdIsInvalid()
	{
		// Arrange
		var parcelId = "";
		var parcelTrackerHistoryDto = new List<ParcelTrackerHistoryEntity>() { };
		var parcelTrackerDto = new ParcelTrackerEntity();
		_inMemoryParcelTrackersRepositoryMock.
			Setup(x => x.GetTrackingDetails(parcelId)).
			ReturnsAsync(parcelTrackerHistoryDto);
		_iHttpParcelTrackingRepositoryMock.
			Setup(x => x.GetTrackingDetails(parcelId)).
			ReturnsAsync(parcelTrackerDto);

		// Act
		var result = await _getParcelTrackingDetailsById.GetTrackingDetailsAsync(parcelId);

		// Assert
		Assert.Null(result);
	}
}
