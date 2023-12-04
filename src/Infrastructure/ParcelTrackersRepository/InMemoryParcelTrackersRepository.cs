namespace parcelfy.Infrastructure.ParcelTrackersRepository;

public class InMemoryParcelTrackersRepository(ParcelfyDbContext dbContext) : IInMemoryParcelTrackingRepository
{
	private readonly ParcelfyDbContext _dbContext = dbContext;

	public Task<IEnumerable<ParcelTrackerHistoryDto>> GetTrackingDetails(string parcelId)
	{
		IEnumerable<ParcelTrackerHistoryDto> parcelTrackHistories = _dbContext.ParcelTrackHistories.Where(p => p.ParcelId == parcelId).ToList();
		return Task.FromResult(parcelTrackHistories);
	}

	public void PostTrackingDetailsHistory(ParcelTrackerHistoryDto parcelTrackerHistoryDto)
	{
		_dbContext.ParcelTrackHistories.Add(parcelTrackerHistoryDto);
		_dbContext.SaveChanges();
	}

}
