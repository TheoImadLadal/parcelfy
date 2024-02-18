using FluentValidation;
using parcelfy.Core.DTOs;

namespace parcelfy.Core.Validators;

public class ParcelTrackerHistoryValidator : AbstractValidator<ParcelTrackerHistoryDTO>
{
	public ParcelTrackerHistoryValidator()
	{
		RuleFor(p => p.parcelid).NotEmpty().Length(13, 16);
		RuleFor(p => p.eventcode).NotEmpty().Length(3);
		RuleFor(p => p.eventmessage).NotEmpty();
		RuleFor(p => p.eventdate).NotEmpty();
		RuleFor(p => p.product).NotEmpty();
		RuleFor(p => p.isfinal).NotEmpty();
		RuleFor(p => p.url).NotEmpty();
	}
}

