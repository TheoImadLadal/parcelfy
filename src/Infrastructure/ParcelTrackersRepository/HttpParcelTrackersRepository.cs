namespace parcelfy.Infrastructure.ParcelTrackersRepository;

public class HttpParcelTrackersRepository(IOptions<LaPosteApiConfiguration> parcelyApiConfiguration, IHttpClientFactory httpClientFactory) : IHttpParcelTrackingRepository
{
	private readonly IOptions<LaPosteApiConfiguration> _parcelyApiConfiguration = parcelyApiConfiguration;
	private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

	public async Task<ParcelTrackerDto> GetTrackingDetails(string parcelId)
	{
		ParcelTrackerDto result = null;
		string key = HandleParameters();

		HttpClient httpClient = _httpClientFactory.CreateClient(Constants.LaPoste);
		httpClient.DefaultRequestHeaders.Add("X-Okapi-Key", key);
		httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		Uri uri = new($"{httpClient.BaseAddress}{_parcelyApiConfiguration.Value.Url}{_parcelyApiConfiguration.Value.Route}{parcelId}");

		HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uri);

		if (httpResponseMessage is null || !httpResponseMessage.IsSuccessStatusCode || httpResponseMessage.Content is null)
		{
			throw new InvalidOperationException();
		}

		string content = await httpResponseMessage.Content.ReadAsStringAsync();
		if (!string.IsNullOrEmpty(content))
		{
			result = JsonSerializer.Deserialize<ParcelTrackerDto>(content);
		}
		return result;
	}

	private string HandleParameters()
	{

		string xOkapiKey = _parcelyApiConfiguration.Value.XOkapiKey ?? string.Empty;
		if (string.IsNullOrEmpty(xOkapiKey))
		{
			string message = $"the API Key is missing";
			throw new ArgumentNullException(message);
		}
		return xOkapiKey;
	}
}
