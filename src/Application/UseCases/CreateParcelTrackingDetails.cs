using FluentValidation;
using FluentValidation.Results;

namespace parcelfy.Application.UseCases;

public class CreateParcelTrackingDetails(IMapper mapper, IInMemoryParcelTrackingRepository inMemoryParcelTrackersRepository, IValidator<ParcelTrackerHistoryDTO> validator) : ICreateParcelTrackingDetails
{
	private readonly IMapper _mapper = mapper;
	private readonly IInMemoryParcelTrackingRepository _inMemoryParcelTrackersRepository = inMemoryParcelTrackersRepository;
	private readonly IValidator<ParcelTrackerHistoryDTO> _validator = validator;

	public async Task CreateTrackingDetailsHistoryAsync(ParcelTrackerHistoryDTO parcelTrackerHistory)
	{
		ValidationResult validationResult = await _validator.ValidateAsync(parcelTrackerHistory);
		if (!validationResult.IsValid)
		{
			throw new ValidationException(validationResult.Errors);
		}

		ParcelTrackerHistoryEntity parcelTrackerHistoryDto = _mapper.Map<ParcelTrackerHistoryEntity>(parcelTrackerHistory);
		_inMemoryParcelTrackersRepository.PostTrackingDetailsHistory(parcelTrackerHistoryDto);
	}
}
