namespace parcelfy.Application.UseCases;

public class GetParcelTrackingDetails(IMapper mapper, IHttpParcelTrackingRepository httpParcelTrackingRepository, IInMemoryParcelTrackingRepository inMemoryParcelTrackersRepository) : IGetParcelTrackingDetails
{
	private readonly IMapper _mapper = mapper;
	private readonly IHttpParcelTrackingRepository _httpParcelTrackingRepository = httpParcelTrackingRepository;
	private readonly IInMemoryParcelTrackingRepository _inMemoryParcelTrackersRepository = inMemoryParcelTrackersRepository;

	public async Task<ParcelTrackerDTO> GetTrackingDetailsAsync(string parcelId)
	{
		IEnumerable<ParcelTrackerHistoryEntity> parcelTrackerHistoryEntities = await _inMemoryParcelTrackersRepository.GetTrackingDetails(parcelId);

		if (parcelTrackerHistoryEntities.Any())
		{
			return MapHistoryToDTO(parcelTrackerHistoryEntities);
		}

		ParcelTrackerEntity parcelTrackerEntity = await _httpParcelTrackingRepository.GetTrackingDetails(parcelId);
		if (parcelTrackerEntity is not null)
		{
			return MapSingleToDTO(parcelTrackerEntity);
		}

		return new ParcelTrackerDTO();
	}

	private ParcelTrackerDTO MapHistoryToDTO(IEnumerable<ParcelTrackerHistoryEntity> parcelTrackerHistoryEntities)
	{
		ParcelTrackerDTO parcelTracker = null;
		List<Event> events = [];

		events.AddRange(parcelTrackerHistoryEntities.Select(parcelTrackerHistoryEntity => new Event()
		{
			Code = parcelTrackerHistoryEntity.EventCode,
			Label = parcelTrackerHistoryEntity.EventMessage,
			Date = parcelTrackerHistoryEntity.EventDate,
		}));

		parcelTracker = _mapper.Map<ParcelTrackerDTO>(parcelTrackerHistoryEntities.FirstOrDefault());
		parcelTracker.Shipment.Event.AddRange(events);
		return parcelTracker;
	}

	private ParcelTrackerDTO MapSingleToDTO(ParcelTrackerEntity parcelTrackerEntity)
	{
		return _mapper.Map<ParcelTrackerDTO>(parcelTrackerEntity);
	}

}
