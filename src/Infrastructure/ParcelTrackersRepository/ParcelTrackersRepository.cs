using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using parcelfy.Infrastructure.ParcelTrackersRepository.Abstractions;
using parcelfy.Infrastructure.ParcelTrackersRepository.Dtos;

namespace parcelfy.Infrastructure.ParcelTrackersRepository;

public class ParcelTrackersRepository : IParcelTrackingRepository
{
    private readonly LaPosteApiConfiguration _parcelyApiConfiguration;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<ParcelTrackersRepository> _logger;

    public ParcelTrackersRepository(IOptions<LaPosteApiConfiguration>? parcelyApiConfiguration, IHttpClientFactory httpClientFactory, ILogger<ParcelTrackersRepository> logger)
    {
        _parcelyApiConfiguration = parcelyApiConfiguration?.Value ?? throw new ArgumentNullException(nameof(parcelyApiConfiguration));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<ParcelTrackerDTO?> GetTrackingDetails(string parcelId)
    {
        ParcelTrackerDTO? result = null;
        string xOkapiKey = _parcelyApiConfiguration.XOkapiKey ?? string.Empty;              
        if (string.IsNullOrEmpty(xOkapiKey))
        {
            string message = $"the API Key is missing";
            throw new NullReferenceException(message); 
        }

        HttpClient httpClient = _httpClientFactory.CreateClient(Constants.LaPoste);
        httpClient.DefaultRequestHeaders.Add("X-Okapi-Key", xOkapiKey);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Uri uri = new($"{httpClient.BaseAddress}{_parcelyApiConfiguration.Url}{_parcelyApiConfiguration.Route}{parcelId}");

        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uri).ConfigureAwait(false);

        if (httpResponseMessage is null || !httpResponseMessage.IsSuccessStatusCode || httpResponseMessage.Content is null)
        {
            string message = $"Failed to get tracking details, from the tiers Api, for the parcel id '{parcelId}': {httpResponseMessage?.ReasonPhrase}, {contentMessage}";
            
            _logger.LogError(message);
           throw new InvalidOperationException(message);
        }

        string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
        if (!string.IsNullOrEmpty(content))
        {
            result = JsonSerializer.Deserialize<ParcelTrackerDTO?>(content);
        }
        return result;
    }


}
