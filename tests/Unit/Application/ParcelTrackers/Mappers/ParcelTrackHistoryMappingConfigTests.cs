using System;
using System.Collections.Generic;
using Mapster;
using MapsterMapper;
using Moq;
using parcelfy.Application.ParcelTrackers.Mappers;
using parcelfy.Application.ParcelTrackers.Models;
using parcelfy.Infrastructure.ParcelTrackersRepository.Dtos;
using Xunit;

namespace Application.ParcelTrackers.Mappers;

public class ParcelTrackHistoryMappingConfigTests
{
	protected static readonly ParcelTrackerHistory ExpectedParcelTrackerhistory = new()
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
		var parcelTrackerHistoryDto = ExpectedParcelTrackerhistory.Adapt<ParcelTrackerHistoryDto>(config);
		var parcelTracker = parcelTrackerHistoryDto.Adapt<ParcelTracker>(config);

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
