using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using parcelfy.Infrastructure.ParcelTrackersRepository;
using parcelfy.Infrastructure.ParcelTrackersRepository.Dtos;
using parcelfy.Infrastructure.Persistence;
using Xunit;

namespace Infrastructure.ParcelTrackersRepository;

public class InMemoryParcelTrackersRepositoryTests
{
	protected readonly Mock<ParcelfyDbContext> _dbContextMock;

	protected InMemoryParcelTrackersRepository _inMemoryParcelTrackersRepository;

	public InMemoryParcelTrackersRepositoryTests()
	{
		_dbContextMock = new Mock<ParcelfyDbContext>();

		_inMemoryParcelTrackersRepository = new InMemoryParcelTrackersRepository(
			_dbContextMock.Object
			);
	}

	protected static readonly List<ParcelTrackerHistoryDto> ExpectedParcelTrackerHistorydto = new List<ParcelTrackerHistoryDto>
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
	

	[Fact]
	public async Task GetTrackingDetailsHistory_Returns_Expected_Result()
	{
		// Arrange
		string parcelId = "LU680211095FR";
		_dbContextMock.Setup(x => x.ParcelTrackHistories).Returns(ExpectedParcelTrackerHistorydto.AsQueryable().BuildMockDbSet());

		// Act
		var result = await _inMemoryParcelTrackersRepository.GetTrackingDetails(parcelId);

		// Assert
		Assert.Equal(ExpectedParcelTrackerHistorydto, result);
	}

	[Fact]
	public void PostTrackingDetailsHistory_To_DbContext()
	{
		// Arrange
		_dbContextMock.Setup(x => x.ParcelTrackHistories).Returns(ExpectedParcelTrackerHistorydto.AsQueryable().BuildMockDbSet());

		// Act
		_inMemoryParcelTrackersRepository.PostTrackingDetailsHistory(ExpectedParcelTrackerHistorydto.First());

		// Assert
		_dbContextMock.Verify(x => x.ParcelTrackHistories.Add(ExpectedParcelTrackerHistorydto.First()), Times.Once);
		_dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
	}

}
