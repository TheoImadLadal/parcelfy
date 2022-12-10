using parcelfy.Application.Common.Interfaces;

namespace parcelfy.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
