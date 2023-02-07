namespace parcelfy.Application.ParcelTrackers.Abstractions;

public interface ICreateParcelTrackingDetailsCommands
{
	Task CreateTrackingDetailsHistoryAsync(ParcelTrackerHistory parcelTrackerHistory);
}
