using static parcelfy.Application.ParcelTrackers.Models.ParcelTracker;

namespace parcelfy.Application.ParcelTrackers.Queries.GetParcelTrackers;

public class GetParcelTrackingDetailsById(IMapper mapper, IHttpParcelTrackingRepository httpParcelTrackingRepository, IInMemoryParcelTrackingRepository inMemoryParcelTrackersRepository) : IGetParcelTrackingDetailsById
{

	private readonly IMapper _mapper = mapper;
	private readonly IHttpParcelTrackingRepository _httpParcelTrackingRepository = httpParcelTrackingRepository;
	private readonly IInMemoryParcelTrackingRepository _inMemoryParcelTrackersRepository = inMemoryParcelTrackersRepository;

	public async Task<ParcelTracker> GetTrackingDetailsAsync(string parcelId)
	{
		return await GetAllTrackingDetailsAsync(parcelId);
	}


	private async Task<ParcelTracker> GetAllTrackingDetailsAsync(string parcelId)
	{
		ParcelTracker parcelTracker = null;
		List<Event> events = [];

		IEnumerable<ParcelTrackerHistoryDto> parcelTrackerHistoryDtos = await _inMemoryParcelTrackersRepository.GetTrackingDetails(parcelId);

		if (parcelTrackerHistoryDtos.Any())
		{
			foreach (var parcelTrackerHistoryDto in parcelTrackerHistoryDtos)
			{
				events.Add(new Event()
				{
					Code = parcelTrackerHistoryDto.EventCode,
					Label = parcelTrackerHistoryDto.EventMessage,
					Date = parcelTrackerHistoryDto.EventDate,
				});
			}

			parcelTracker = _mapper.Map<ParcelTracker>(parcelTrackerHistoryDtos.FirstOrDefault());
			parcelTracker.Shipment.Event.AddRange(events);
		}

		if (parcelTracker is null)
		{
			ParcelTrackerDto parcelTrackerFromDto = await _httpParcelTrackingRepository.GetTrackingDetails(parcelId);
			if (parcelTrackerFromDto is not null)
			{
				parcelTracker = _mapper.Map<ParcelTracker>(parcelTrackerFromDto);
			}
		}
		return parcelTracker;
	}

}
