namespace Bdd.StepDefinitions;

[Binding]
public class ParcelTrackingStepDefinitions
{
	private readonly Mock<ParcelfyDbContext> _dbContextMock;
	private readonly InMemoryParcelTrackersRepository _inMemoryParcelTrackersRepository;
	public IEnumerable<ParcelTrackerHistoryDto> _result;
	protected static readonly List<ParcelTrackerHistoryDto> _parcelTrackerHistorydto = new List<ParcelTrackerHistoryDto>
	{
		new ParcelTrackerHistoryDto
		{
			ParcelId = "LU680211095FR",
			EventCode = "DR1",
			EventMessage = "La Poste est pr�te � prendre en charge votre envoi.D�s qu�il nous sera confi�,",
			EventDate = DateTime.Now,
			Product = "Courrier international",
			IsFinal = false,
			URL = "https://www.laposte.fr/outils/suivre-vos-envois?code=LU680211095FR"
		}
	};

	public ParcelTrackingStepDefinitions()
	{
		_dbContextMock = new Mock<ParcelfyDbContext>();
		_inMemoryParcelTrackersRepository = new InMemoryParcelTrackersRepository(_dbContextMock.Object);
		_result = new List<ParcelTrackerHistoryDto>();
	}

	[Given(@"a parcel with ID ""([^""]*)""")]
	public void GivenAParcelWithID(string p0)
	{
		_parcelTrackerHistorydto.First().ParcelId = p0;
	}

	[Given(@"the parcel has tracking details")]
	public void GivenTheParcelHasTrackingDetails()
	{
		// Add some tracking details to the parcel
		_dbContextMock.Setup(x => x.ParcelTrackHistories).Returns(_parcelTrackerHistorydto.AsQueryable().BuildMockDbSet());
		_inMemoryParcelTrackersRepository.PostTrackingDetailsHistory(_parcelTrackerHistorydto.First());
	}

	[When(@"I call GetTrackingDetails with parcel ID ""([^""]*)""")]
	public async Task WhenICallGetTrackingDetailsWithParcelID(string p0)
	{
		// Call the GetTrackingDetails method and store the result
		_dbContextMock.Setup(x => x.ParcelTrackHistories).Returns(_parcelTrackerHistorydto.AsQueryable().BuildMockDbSet());
		_result = await _inMemoryParcelTrackersRepository.GetTrackingDetails(p0);
	}

	[Then(@"the result should be a list of tracking details")]
	public void ThenTheResultShouldBeAListOfTrackingDetails()
	{
		// Check that the result is not null and is a list of tracking details
		Assert.NotNull(_result);
		Assert.IsType<List<ParcelTrackerHistoryDto>>(_result);
	}

	[Then(@"the result should contain the tracking details for the parcel")]
	public void ThenTheResultShouldContainTheTrackingDetailsForTheParcel()
	{
		// Check that the result contains the tracking details for the parcel
		Assert.True(_result?.Any(t => t.ParcelId == "LZ712917377US" && t.EventCode == "DR1"));
	}
}


