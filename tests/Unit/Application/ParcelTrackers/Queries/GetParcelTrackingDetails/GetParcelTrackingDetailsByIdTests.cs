using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MapsterMapper;
using Moq;
using parcelfy.Application.ParcelTrackers.Models;
using parcelfy.Application.ParcelTrackers.Queries.GetParcelTrackers;
using parcelfy.Infrastructure.ParcelTrackersRepository.Abstractions;
using parcelfy.Infrastructure.ParcelTrackersRepository.Dtos;
using Xunit;

namespace Application.ParcelTrackers.Queries.GetParcelTrackers;

public class GetParcelTrackingDetailsByIdTests
{
	protected readonly Mock<IMapper> _mapperMock;
	protected readonly Mock<IInMemoryParcelTrackingRepository> _inMemoryParcelTrackersRepositoryMock;
	protected readonly Mock<IHttpParcelTrackingRepository> _iHttpParcelTrackingRepositoryMock;

	protected GetParcelTrackingDetailsById _getParcelTrackingDetailsById;

	public GetParcelTrackingDetailsByIdTests()
	{
		_mapperMock	= new Mock<IMapper>();
		_iHttpParcelTrackingRepositoryMock = new Mock<IHttpParcelTrackingRepository>();
		_inMemoryParcelTrackersRepositoryMock = new Mock<IInMemoryParcelTrackingRepository>();

		_getParcelTrackingDetailsById = new GetParcelTrackingDetailsById(
			_mapperMock.Object,
			_iHttpParcelTrackingRepositoryMock.Object,
			_inMemoryParcelTrackersRepositoryMock.Object
			);
	}

	protected static readonly List<ParcelTrackerHistoryDto> ExpectedParcelTrackerHistorydto = new()
	{
		new ParcelTrackerHistoryDto
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

	protected static readonly ParcelTrackerDto ExpectedParcelTrackerdto = new()
	{
		IdShip = "LU680211095FR",
		ReturnCode = 200,
		ReturnMessage = "Votre suivi",
		Shipment = new ParcelTrackerDto.ShipmentDto
		{
			Product = "Courrier international",
			Url = "https://www.laposte.fr/outils/suivre-vos-envois?code=LU680211095FR",
			IdShip = "LU680211095FR",
			IsFinal = true,
			Event = new List<ParcelTrackerDto.Event>
			{
				new ParcelTrackerDto.Event
				{
					Code = "DR1",
					Date = System.DateTime.Now,
					Label = "La Poste est prête à prendre en charge votre envoi. Dès qu’il nous sera confié, vous pourrez suivre son trajet ici."
				}
			}
		}
	};

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

	[Fact]
    public async Task GetTrackingDetailsAsync_ShouldReturnParcelTracker_WhenDataIsAvailableInInMemoryRepository()
	{
		// Arrange
		var parcelId = "LU680211095FR";
		_inMemoryParcelTrackersRepositoryMock.
			Setup(x => x.GetTrackingDetails(parcelId)).
			ReturnsAsync(ExpectedParcelTrackerHistorydto);
		_mapperMock.
			Setup(x => x.Map<ParcelTracker>(ExpectedParcelTrackerHistorydto[0])).
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
		var parcelTrackerHistoryDto = new List<ParcelTrackerHistoryDto>() { };
		_inMemoryParcelTrackersRepositoryMock.
			Setup(x => x.GetTrackingDetails(parcelId)).
			ReturnsAsync(parcelTrackerHistoryDto);
		_iHttpParcelTrackingRepositoryMock.
			Setup(x => x.GetTrackingDetails(parcelId)).
			ReturnsAsync(ExpectedParcelTrackerdto);
		_mapperMock.
			Setup(x => x.Map<ParcelTracker>(ExpectedParcelTrackerdto)).
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
		var parcelTrackerHistoryDto = new List<ParcelTrackerHistoryDto>() { };
		var parcelTrackerDto = new ParcelTrackerDto();
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
