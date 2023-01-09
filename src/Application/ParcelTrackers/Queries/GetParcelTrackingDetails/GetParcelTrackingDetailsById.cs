using static parcelfy.Application.ParcelTrackers.Models.ParcelTracker;

namespace parcelfy.Application.ParcelTrackers.Queries.GetParcelTrackers;

public class GetParcelTrackingDetailsById : IGetParcelTrackingDetailsById
{

	private readonly IMapper _mapper;
	private readonly IHttpParcelTrackingRepository _httpParcelTrackingRepository;
	private readonly IInMemoryParcelTrackingRepository _inMemoryParcelTrackersRepository;

	public GetParcelTrackingDetailsById(IMapper mapper,	IHttpParcelTrackingRepository httpParcelTrackingRepository,	IInMemoryParcelTrackingRepository inMemoryParcelTrackersRepository)
	{
		_mapper = mapper;
		_httpParcelTrackingRepository = httpParcelTrackingRepository;
		_inMemoryParcelTrackersRepository = inMemoryParcelTrackersRepository;
	}

	public async Task<ParcelTracker> GetTrackingDetailsAsync(string parcelId)
	{
		return await GetAllTrackingDetailsAsync(parcelId).ConfigureAwait(false);		
	}


	private async Task<ParcelTracker> GetAllTrackingDetailsAsync(string parcelId)
	{
		await Task.CompletedTask;

		ParcelTracker parcelTracker = null;
		List<Event> trackingEvents = new();

		IEnumerable<ParcelTrackHistory> parcelTrackersFromDb = _inMemoryParcelTrackersRepository.GetTrackingDetails(parcelId);

		if (parcelTrackersFromDb.Any())
		{
			foreach(var parcelTrackerFromDb in parcelTrackersFromDb)
			{
				trackingEvents.Add(new Event()
				{
					Code = parcelTrackerFromDb.EventCode,
					Label = parcelTrackerFromDb.EventMessage,
					Date = parcelTrackerFromDb.EventDate,
				});
			}

			parcelTracker = _mapper.Map<ParcelTracker>(parcelTrackersFromDb.FirstOrDefault());
			parcelTracker.Shipment.Event.AddRange(trackingEvents);
		}

		if (parcelTracker is null)
		{
			ParcelTrackerDto parcelTrackerFromDto = await _httpParcelTrackingRepository.GetTrackingDetails(parcelId).ConfigureAwait(false);
			if (parcelTrackerFromDto is not null)
			{
				parcelTracker = _mapper.Map<ParcelTracker>(parcelTrackerFromDto);
			}
		}
		return parcelTracker;
	}

}
