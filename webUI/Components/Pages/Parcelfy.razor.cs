using Microsoft.AspNetCore.Components;
using webUI.Components.Data;
using webUI.Components.Models;

namespace webUI.Components.Pages;

public partial class Parcelfy
{
	private ParcelTrackerRequest ParcelTrackerRequest = new();
	private ParcelTracker ParcelTrackerResponse = new();

	[Inject]
	public IParcelTrackerApiService? ParcelTrackerApiService { get; set; }

	private async Task HandleValidSubmit()
	{
		ParcelTrackerResponse = await ParcelTrackerApiService.GetParcelTrackingDetails(ParcelTrackerRequest.ParcelId);
	}
}


