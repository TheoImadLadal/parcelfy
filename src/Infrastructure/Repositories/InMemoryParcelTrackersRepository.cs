namespace parcelfy.Infrastructure.Repositories;

public class InMemoryParcelTrackersRepository(ParcelfyDbContext dbContext) : IInMemoryParcelTrackingRepository
{
	private readonly ParcelfyDbContext _dbContext = dbContext;

	public Task<IEnumerable<ParcelTrackerHistoryEntity>> GetTrackingDetails(string parcelId)
	{
		IEnumerable<ParcelTrackerHistoryEntity> parcelTrackHistories = _dbContext.ParcelTrackHistories.Where(p => p.parcelid == parcelId).ToList();
		return Task.FromResult(parcelTrackHistories);
	}

	public void PostTrackingDetailsHistory(ParcelTrackerHistoryEntity parcelTrackerHistoryDto)
	{
		_dbContext.ParcelTrackHistories.Add(parcelTrackerHistoryDto);
		_dbContext.SaveChanges();
	}

}
