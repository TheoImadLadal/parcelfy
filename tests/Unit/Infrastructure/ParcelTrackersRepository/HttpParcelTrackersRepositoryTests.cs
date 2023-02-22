﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using parcelfy.Infrastructure;
using parcelfy.Infrastructure.ParcelTrackersRepository;
using parcelfy.Infrastructure.ParcelTrackersRepository.Dtos;
using Xunit;

namespace Infrastructure.ParcelTrackersRepository;

public class HttpParcelTrackersRepositoryTests
{
	protected readonly IOptions<LaPosteApiConfiguration> _parcelyApiConfigurationMock;
	protected readonly Mock<IHttpClientFactory> _httpClientFactoryMock;

	protected HttpParcelTrackersRepository _httpParcelTrackersRepository;

	public HttpParcelTrackersRepositoryTests()
	{
		_httpClientFactoryMock = new Mock<IHttpClientFactory>();
		_parcelyApiConfigurationMock = Options.Create(
			GetApiConfiguration()
			);

		_httpParcelTrackersRepository = new HttpParcelTrackersRepository(
			_parcelyApiConfigurationMock,
			_httpClientFactoryMock.Object
			);
	}

	protected static LaPosteApiConfiguration GetApiConfiguration()
	{
		return new LaPosteApiConfiguration
		{
			XOkapiKey = "some-api-key",
			Url = new Uri("https://api.laposte.fr/"),
			Route = "tracking/v1/parcels/"
		};
	}

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
					Date = DateTime.Today,
					Label = "La Poste est prête à prendre en charge votre envoi. Dès qu’il nous sera confié, vous pourrez suivre son trajet ici."
				}
			}
		}
	};

	[Fact]
	public async Task GetTrackingDetails_ReturnsParcelTrackerDto()
	{
		// Arrange
		var parcelId = "LU680211095FR";
		var httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
		httpMessageHandlerMock
			.Protected()
			.Setup<Task<HttpResponseMessage>>(
				"SendAsync", 
				ItExpr.IsAny<HttpRequestMessage>(), 
				ItExpr.IsAny<CancellationToken>())
			.ReturnsAsync(new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK,
				Content = new StringContent(JsonSerializer.Serialize(ExpectedParcelTrackerdto)),
			});
		var httpClient = new HttpClient(httpMessageHandlerMock.Object)
		{
			BaseAddress = new Uri("http://test.com/")
		};
		_httpClientFactoryMock.
			Setup(x => x.CreateClient(It.IsAny<string>())).
			Returns(httpClient);

		// Act
		var result = await _httpParcelTrackersRepository.GetTrackingDetails(parcelId);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(ExpectedParcelTrackerdto.IdShip, result.IdShip);
		Assert.Equal(ExpectedParcelTrackerdto.ReturnCode, result.ReturnCode);
		Assert.Equal(ExpectedParcelTrackerdto.ReturnMessage, result.ReturnMessage);
		
		Assert.Equal(ExpectedParcelTrackerdto.Shipment.Url, result.Shipment.Url);
		Assert.Equal(ExpectedParcelTrackerdto.Shipment.IsFinal, result.Shipment.IsFinal);
		Assert.Equal(ExpectedParcelTrackerdto.Shipment.IdShip, result.Shipment.IdShip);
		Assert.Equal(ExpectedParcelTrackerdto.Shipment.Product, result.Shipment.Product);
	}

	[Fact]
	public async Task GetTrackingDetails_ThrowsException_WhenHttpResponseIsNull()
	{
		// Arrange
		var parcelId = "LU680211095FR";
		var httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
		httpMessageHandlerMock
			.Protected()
			.Setup<Task<HttpResponseMessage>>(
				"SendAsync",
				ItExpr.IsAny<HttpRequestMessage>(),
				ItExpr.IsAny<CancellationToken>())
			.ReturnsAsync(new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.NotFound,
				Content = new StringContent(JsonSerializer.Serialize("")),
			});
		var httpClient = new HttpClient(httpMessageHandlerMock.Object)
		{
			BaseAddress = new Uri("http://test.com/")
		};
		_httpClientFactoryMock.
			Setup(x => x.CreateClient(It.IsAny<string>())).
			Returns(httpClient);

		// Act
		var result = await Assert.ThrowsAsync<InvalidOperationException>(() => _httpParcelTrackersRepository.GetTrackingDetails(parcelId));

		// Assert
		Assert.Equal("Operation is not valid due to the current state of the object.", result.Message);
	}

	[Fact]
	public async Task GetTrackingDetails_ThrowsException_WhenApiKeyIsMissing()
	{
		// Arrange
		var parcelId = "LU680211095FR";
		_parcelyApiConfigurationMock.Value.XOkapiKey = null;
		
		// Act
		var result = await Assert.ThrowsAsync<ArgumentNullException>(() => _httpParcelTrackersRepository.GetTrackingDetails(parcelId));

		// Assert
		Assert.Equal("Value cannot be null. (Parameter 'the API Key is missing')", result.Message);
	}
}
