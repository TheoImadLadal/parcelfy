namespace parcelfy.Infrastructure.Repositories;

public class HttpParcelTrackersRepository(IHttpClientFactory httpClientFactory) : IHttpParcelTrackingRepository
{
	private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

	public async Task<ParcelTrackerEntity> GetTrackingDetails(string parcelId)
	{
		ParcelTrackerEntity result = null;
		string key = HandleParameters();

		HttpClient httpClient = _httpClientFactory.CreateClient(Constants.LaPoste);
		httpClient.DefaultRequestHeaders.Add("X-Okapi-Key", key);
		httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		Uri uri = new($"{httpClient.BaseAddress}{Constants.LaPosteUrl}{Constants.LaPosteRoute}{parcelId}");

		HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uri);

		if (httpResponseMessage is null || !httpResponseMessage.IsSuccessStatusCode || httpResponseMessage.Content is null)
		{
			throw new InvalidOperationException();
		}

		string content = await httpResponseMessage.Content.ReadAsStringAsync();
		if (!string.IsNullOrEmpty(content))
		{
			result = JsonSerializer.Deserialize<ParcelTrackerEntity>(content);
		}
		return result;
	}

	private string HandleParameters()
	{

		string xOkapiKey = Constants.LaPosteKey;
		if (string.IsNullOrEmpty(xOkapiKey))
		{
			string message = $"the API Key is missing";
			throw new ArgumentNullException(message);
		}
		return xOkapiKey;
	}
}
