namespace parcelfy.Infrastructure.ParcelTrackersRepository;

public class InMemoryParcelTrackersRepository : IInMemoryParcelTrackingRepository
{
	private readonly ParcelfyDbContext _dbContext;

	public InMemoryParcelTrackersRepository(ParcelfyDbContext dbContext)
	{
		_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
	}

	public IEnumerable<ParcelTrackHistory> GetTrackingDetails(string parcelId)
	{
		IEnumerable<ParcelTrackHistory> parcelTrackHistories = _dbContext.ParcelTrackHistories.Where(p => p.ParcelId == parcelId).ToList();
		return parcelTrackHistories;
	}

}
