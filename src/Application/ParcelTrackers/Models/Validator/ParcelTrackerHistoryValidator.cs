namespace parcelfy.Application.ParcelTrackers.Models;

public class ParcelTrackerHistoryValidator : AbstractValidator<ParcelTrackerHistory>
{
	public ParcelTrackerHistoryValidator()
	{
		RuleFor(p => p.ParcelId).NotEmpty().Length(14,16);
		RuleFor(p => p.EventCode).NotEmpty().Length(3);
		RuleFor(p => p.EventMessage).NotEmpty();
		RuleFor(p => p.EventDate).NotEmpty();
		RuleFor(p => p.Product).NotEmpty();
		RuleFor(p => p.IsFinal).NotEmpty();
		RuleFor(p => p.URL).NotEmpty();
	}
}

