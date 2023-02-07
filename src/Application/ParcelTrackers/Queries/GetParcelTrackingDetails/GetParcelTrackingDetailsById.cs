﻿using static parcelfy.Application.ParcelTrackers.Models.ParcelTracker;

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
		ParcelTracker parcelTracker = null;
		List<Event> events = new();

		IEnumerable<ParcelTrackerHistoryDto> parcelTrackerHistoryDtos = await _inMemoryParcelTrackersRepository.GetTrackingDetails(parcelId).ConfigureAwait(false);

		if (parcelTrackerHistoryDtos.Any())
		{
			foreach(var parcelTrackerHistoryDto in parcelTrackerHistoryDtos)
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
			ParcelTrackerDto parcelTrackerFromDto = await _httpParcelTrackingRepository.GetTrackingDetails(parcelId).ConfigureAwait(false);
			if (parcelTrackerFromDto is not null)
			{
				parcelTracker = _mapper.Map<ParcelTracker>(parcelTrackerFromDto);
			}
		}
		return parcelTracker;
	}

}
