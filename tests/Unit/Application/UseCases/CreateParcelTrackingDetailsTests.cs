using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MapsterMapper;
using Moq;
using parcelfy.Application.UseCases;
using parcelfy.Core.DTOs;
using parcelfy.Core.Entities;
using parcelfy.Core.Repositories;
using Xunit;

namespace parcelfy.tests.Application.UseCases;

public class CreateParcelTrackingDetailsTests
{
	protected readonly Mock<IMapper> _mapperMock;
	protected readonly Mock<IInMemoryParcelTrackingRepository> _inMemoryParcelTrackersRepositoryMock;
	protected readonly Mock<IValidator<ParcelTrackerHistoryDTO>> _validatorMock;

	protected CreateParcelTrackingDetails _createParcelTrackingDetailsCommand;

	public CreateParcelTrackingDetailsTests()
	{
		_mapperMock = new Mock<IMapper>();
		_inMemoryParcelTrackersRepositoryMock = new Mock<IInMemoryParcelTrackingRepository>();
		_validatorMock = new Mock<IValidator<ParcelTrackerHistoryDTO>>();

		_createParcelTrackingDetailsCommand = new CreateParcelTrackingDetails(
			_mapperMock.Object,
			_inMemoryParcelTrackersRepositoryMock.Object,
			_validatorMock.Object
			);
	}

	[Fact]
	public async Task CreateTrackingDetailsHistoryAsync_ShouldSaveTheTrackingDetails()
	{
		// Arrange
		var validDto = new ParcelTrackerHistoryDTO();
		var validation = new ValidationResult();
		_validatorMock.
			Setup(x => x.ValidateAsync(validDto, default)).
			ReturnsAsync(validation);
		_mapperMock.
			Setup(x => x.Map<ParcelTrackerHistoryEntity>(validDto)).
			Returns(new ParcelTrackerHistoryEntity());
		_inMemoryParcelTrackersRepositoryMock.
			Setup(x => x.PostTrackingDetailsHistory(It.IsAny<ParcelTrackerHistoryEntity>()));

		// Act
		await _createParcelTrackingDetailsCommand.CreateTrackingDetailsHistoryAsync(validDto);

		// Assert
		_inMemoryParcelTrackersRepositoryMock.
			Verify(x => x.PostTrackingDetailsHistory(It.IsAny<ParcelTrackerHistoryEntity>()), Times.Once);
	}
}
