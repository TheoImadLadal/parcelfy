namespace parcelfy.Infrastructure.ParcelTrackersRepository;

public class HttpParcelTrackersRepository : IHttpParcelTrackingRepository, IDisposable
{
    private readonly LaPosteApiConfiguration _parcelyApiConfiguration;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<HttpParcelTrackersRepository> _logger;
	private bool _isDisposed = false;

	public HttpParcelTrackersRepository(IOptions<LaPosteApiConfiguration>? parcelyApiConfiguration, IHttpClientFactory httpClientFactory, ILogger<HttpParcelTrackersRepository> logger)
    {
        _parcelyApiConfiguration = parcelyApiConfiguration?.Value ?? throw new ArgumentNullException(nameof(parcelyApiConfiguration));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<ParcelTrackerDto?> GetTrackingDetails(string parcelId)
    {
		ParcelTrackerDto? result = null;
		string key = HandleParameters();

        HttpClient httpClient = _httpClientFactory.CreateClient(Constants.LaPoste);
        httpClient.DefaultRequestHeaders.Add("X-Okapi-Key", key);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Uri uri = new($"{httpClient.BaseAddress}{_parcelyApiConfiguration.Url}{_parcelyApiConfiguration.Route}{parcelId}");

        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uri).ConfigureAwait(false);

        if (httpResponseMessage is null || !httpResponseMessage.IsSuccessStatusCode || httpResponseMessage.Content is null)
        {
            string message = $"Failed to get tracking details, from the tiers Api, for the parcel id '{parcelId}': {httpResponseMessage?.ReasonPhrase}";
            
            _logger.LogError(message);
           throw new InvalidOperationException(message);
        }

        string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
        if (!string.IsNullOrEmpty(content))
        {
            result = JsonSerializer.Deserialize<ParcelTrackerDto?>(content);
        }
        return result;
    }

	private string HandleParameters()
	{
		
		string xOkapiKey = _parcelyApiConfiguration.XOkapiKey ?? string.Empty;
		if (string.IsNullOrEmpty(xOkapiKey))
		{
			string message = $"the API Key is missing";
			throw new ArgumentNullException(message);
		}
		return xOkapiKey;
	}

	#region DISPOSE
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (_isDisposed)
			return;

		if (disposing)
		{
			Dispose();
		}

		_isDisposed = true;
	}
	#endregion


}
