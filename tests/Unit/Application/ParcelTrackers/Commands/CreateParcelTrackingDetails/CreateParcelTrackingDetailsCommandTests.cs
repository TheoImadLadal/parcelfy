using System.Threading.Tasks;
using MapsterMapper;
using Moq;
using parcelfy.Application.ParcelTrackers.Commands.CreateParcelTrackingDetails;
using parcelfy.Application.ParcelTrackers.Models;
using parcelfy.Infrastructure.ParcelTrackersRepository.Abstractions;
using parcelfy.Infrastructure.ParcelTrackersRepository.Dtos;
using Xunit;

namespace Application.ParcelTrackers.Commands.CreateParcelTrackingDetails;

public class CreateParcelTrackingDetailsCommandTests
{
	protected readonly Mock<IMapper> _mapperMock;
	protected readonly Mock<IInMemoryParcelTrackingRepository> _inMemoryParcelTrackersRepositoryMock;
	
	protected CreateParcelTrackingDetailsCommand _createParcelTrackingDetailsCommand;

	public CreateParcelTrackingDetailsCommandTests()
	{
		_mapperMock = new Mock<IMapper>();
		_inMemoryParcelTrackersRepositoryMock = new Mock<IInMemoryParcelTrackingRepository>();

		_createParcelTrackingDetailsCommand = new CreateParcelTrackingDetailsCommand(
			_mapperMock.Object,
			_inMemoryParcelTrackersRepositoryMock.Object
			);
	}

	[Fact]
	public async Task CreateTrackingDetailsHistoryAsync_ShouldSaveTheTrackingDetails()
	{
		// Arrange
		_mapperMock.
			Setup(x => x.Map<ParcelTrackerHistoryDto>(new ParcelTrackerHistory())).
			Returns(new ParcelTrackerHistoryDto());
		_inMemoryParcelTrackersRepositoryMock.
			Setup(x => x.PostTrackingDetailsHistory(It.IsAny<ParcelTrackerHistoryDto>()));

		// Act
		await _createParcelTrackingDetailsCommand.CreateTrackingDetailsHistoryAsync(new ParcelTrackerHistory());

		// Assert
		_inMemoryParcelTrackersRepositoryMock.
			Verify(x => x.PostTrackingDetailsHistory(It.IsAny<ParcelTrackerHistoryDto>()), Times.Once);
	}
}
