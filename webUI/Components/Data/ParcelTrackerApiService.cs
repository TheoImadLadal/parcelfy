using System.Text;
using System.Text.Json;
using webUI.Components.Models;

namespace webUI.Components.Data;

public class ParcelTrackerApiService(IHttpClientFactory clientFactory) : IParcelTrackerApiService
{
	private readonly IHttpClientFactory _clientFactory = clientFactory;

	public async Task<ParcelTracker?> GetParcelTrackingDetails(string parcelId)
	{
		ParcelTracker result = new();
		string? tokenResponse = string.Empty;

		// Ask for a token
		string tokenUrl = "https://parcelfy.azurewebsites.net/token";
		var tokenRequest = new HttpRequestMessage(HttpMethod.Post, tokenUrl);
		tokenRequest.Headers.Add("Accept", "application/vnd.github.v3+json");
		tokenRequest.Content =
			new StringContent(
				JsonSerializer.Serialize(new { username = "parcelfy", password = "parcelfy123" }),
				Encoding.UTF8,
				"application/json");

		var tokenClient = _clientFactory.CreateClient();

		var tokenClientResponse = await tokenClient.SendAsync(tokenRequest);
		if (tokenClientResponse is not null)
		{
			tokenResponse = JsonSerializer.Deserialize<string>(await tokenClientResponse.Content.ReadAsStringAsync());
		}


		// Ask for the parcel 
		var parcelUrl = string.Format("https://parcelfy.azurewebsites.net/parcel-tracker/{0}", parcelId);
		var parcelRequest = new HttpRequestMessage(HttpMethod.Get, parcelUrl);
		parcelRequest.Headers.Add("Accept", "application/json");
		parcelRequest.Headers.Add("Authorization", $"Bearer {tokenResponse}");
		var parcelClient = _clientFactory.CreateClient();

		var parcelClientResponse = await parcelClient.SendAsync(parcelRequest);
		if (parcelClientResponse.IsSuccessStatusCode)
		{
			string stringResponse = await parcelClientResponse.Content.ReadAsStringAsync();
			result = JsonSerializer.Deserialize<ParcelTracker>(
				stringResponse,
				new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

		}

		return result;
	}
}
