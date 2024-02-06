using System;
using Mapster;
using parcelfy.Application.Mappers;
using parcelfy.Core.DTOs;
using parcelfy.Core.Entities;
using Xunit;

namespace parcelfy.tests.Application.Mappers;

public class ParcelTrackHistoryMappingConfigTests
{
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

	[Fact]
	public void TestMappingConfig()
	{
		// Arrange
		TypeAdapterConfig config = new TypeAdapterConfig();
		new ParcelTrackHistoryMappingConfig().Register(config);

		// Act
		var parcelTrackerHistoryDto = ExpectedParcelTrackerhistory.Adapt<ParcelTrackerHistoryEntity>(config);
		var parcelTracker = parcelTrackerHistoryDto.Adapt<ParcelTrackerDTO>(config);

		// Assert
		Assert.Equal(ExpectedParcelTrackerhistory.ParcelId, parcelTrackerHistoryDto.ParcelId);
		Assert.Equal(ExpectedParcelTrackerhistory.URL, parcelTrackerHistoryDto.URL);
		Assert.Equal(ExpectedParcelTrackerhistory.Product, parcelTrackerHistoryDto.Product);
		Assert.Equal(ExpectedParcelTrackerhistory.IsFinal, parcelTrackerHistoryDto.IsFinal);

		Assert.Equal(parcelTrackerHistoryDto.ParcelId, parcelTracker.IdShip);
		Assert.Equal(parcelTrackerHistoryDto.URL, parcelTracker.Shipment.Url);
		Assert.Equal(parcelTrackerHistoryDto.Product, parcelTracker.Shipment.Product);
		Assert.Equal(parcelTrackerHistoryDto.IsFinal, parcelTracker.Shipment.IsFinal);
		Assert.Equal(parcelTrackerHistoryDto.ParcelId, parcelTracker.Shipment.IdShip);
	}
}
