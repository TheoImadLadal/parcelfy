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
		parcelid = "LU680211095FR",
		eventcode = "DR1",
		eventmessage = "La Poste est prête à prendre en charge votre envoi.Dès qu’il nous sera confié,",
		eventdate = DateTime.Now,
		product = "Courrier international",
		isfinal = false,
		url = "https://www.laposte.fr/outils/suivre-vos-envois?code=LU680211095FR"
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
		Assert.Equal(ExpectedParcelTrackerhistory.parcelid, parcelTrackerHistoryDto.parcelid);
		Assert.Equal(ExpectedParcelTrackerhistory.url, parcelTrackerHistoryDto.url);
		Assert.Equal(ExpectedParcelTrackerhistory.product, parcelTrackerHistoryDto.product);
		Assert.Equal(ExpectedParcelTrackerhistory.isfinal, parcelTrackerHistoryDto.isfinal);

		Assert.Equal(parcelTrackerHistoryDto.parcelid, parcelTracker.IdShip);
		Assert.Equal(parcelTrackerHistoryDto.url, parcelTracker.Shipment.Url);
		Assert.Equal(parcelTrackerHistoryDto.product, parcelTracker.Shipment.Product);
		Assert.Equal(parcelTrackerHistoryDto.isfinal, parcelTracker.Shipment.IsFinal);
		Assert.Equal(parcelTrackerHistoryDto.parcelid, parcelTracker.Shipment.IdShip);
	}
}
