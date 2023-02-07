namespace parcelfy.Application.ParcelTrackers.Commands.CreateParcelTrackingDetails;

public class CreateParcelTrackingDetailsCommand : ICreateParcelTrackingDetailsCommands
{

	private readonly IMapper _mapper;
	private readonly IInMemoryParcelTrackingRepository _inMemoryParcelTrackersRepository;

	public CreateParcelTrackingDetailsCommand(IMapper mapper, IInMemoryParcelTrackingRepository inMemoryParcelTrackersRepository)
	{
		_mapper = mapper;
		_inMemoryParcelTrackersRepository = inMemoryParcelTrackersRepository;
	}

	public Task CreateTrackingDetailsHistoryAsync(ParcelTrackerHistory parcelTrackerHistory)
	{

		ParcelTrackerHistoryDto parcelTrackerHistoryDto = _mapper.Map<ParcelTrackerHistoryDto>(parcelTrackerHistory);
		_inMemoryParcelTrackersRepository.PostTrackingDetailsHistory(parcelTrackerHistoryDto);
		return Task.CompletedTask;
	}
}
