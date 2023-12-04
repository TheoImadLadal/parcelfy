namespace parcelfy.Application.ParcelTrackers.Commands.CreateParcelTrackingDetails;

public class CreateParcelTrackingDetailsCommand(IMapper mapper, IInMemoryParcelTrackingRepository inMemoryParcelTrackersRepository) : ICreateParcelTrackingDetailsCommands
{

	private readonly IMapper _mapper = mapper;
	private readonly IInMemoryParcelTrackingRepository _inMemoryParcelTrackersRepository = inMemoryParcelTrackersRepository;

	public Task CreateTrackingDetailsHistoryAsync(ParcelTrackerHistory parcelTrackerHistory)
	{

		ParcelTrackerHistoryDto parcelTrackerHistoryDto = _mapper.Map<ParcelTrackerHistoryDto>(parcelTrackerHistory);
		_inMemoryParcelTrackersRepository.PostTrackingDetailsHistory(parcelTrackerHistoryDto);
		return Task.CompletedTask;
	}
}
