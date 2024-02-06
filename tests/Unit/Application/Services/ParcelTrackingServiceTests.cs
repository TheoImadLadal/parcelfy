using System.Threading.Tasks;
using Moq;
using parcelfy.Application.Services;
using parcelfy.Application.UseCases;
using parcelfy.Core.DTOs;
using Xunit;

namespace parcelfy.tests.Application.Services;

public class ParcelTrackingServiceTests
{
	[Fact]
	public async Task GetTrackingDetailsById_ShouldCall_GetTrackingDetailsAsync()
	{
		// Arrange
		var getParcelTrackingDetailsMock = new Mock<IGetParcelTrackingDetails>();
		var createParcelTrackingDetailsMock = new Mock<ICreateParcelTrackingDetails>();

		var parcelId = "123";
		var parcelTrackingServiceTests = new ParcelTrackingService(getParcelTrackingDetailsMock.Object, createParcelTrackingDetailsMock.Object);

		// Act
		await parcelTrackingServiceTests.GetTrackingDetailsById(parcelId);

		// Assert
		getParcelTrackingDetailsMock.Verify(x => x.GetTrackingDetailsAsync(parcelId), Times.Once);
	}

	[Fact]
	public async Task CreateTrackingDetails_ShouldCall_CreateTrackingDetailsHistoryAsync()
	{
		// Arrange
		var getParcelTrackingDetailsMock = new Mock<IGetParcelTrackingDetails>();
		var createParcelTrackingDetailsMock = new Mock<ICreateParcelTrackingDetails>();

		var parcelTrackerHistory = new ParcelTrackerHistoryDTO();
		var parcelTrackingServiceTests = new ParcelTrackingService(getParcelTrackingDetailsMock.Object, createParcelTrackingDetailsMock.Object);

		// Act
		await parcelTrackingServiceTests.CreateTrackingDetails(parcelTrackerHistory);

		// Assert
		createParcelTrackingDetailsMock.Verify(x => x.CreateTrackingDetailsHistoryAsync(parcelTrackerHistory), Times.Once);
	}
}
