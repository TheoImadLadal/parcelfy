using System.Globalization;
using System.Text.Json;
using Api;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace parcelfy.Controllers;

[ApiController]
[Route("[controller]")]
public class ParcelTrackerController : ControllerBase
{
    private readonly ILogger<ParcelTrackerController> _logger;
    private readonly LaPosteApiConfiguration _parcelyApiConfiguration;
    private readonly IHttpClientFactory _httpClientFactory;

    public ParcelTrackerController(ILogger<ParcelTrackerController>? logger, IOptions<LaPosteApiConfiguration>? parcelyApiConfiguration, IHttpClientFactory httpClientFactory)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _parcelyApiConfiguration = parcelyApiConfiguration?.Value ?? throw new ArgumentNullException(nameof(parcelyApiConfiguration));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }


    [HttpGet("{parcelId}")] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ParcelTracker?>> ParcelToTrack(string parcelId)
    {
        try
        {
            if (string.IsNullOrEmpty(parcelId))
            {
                return BadRequest();
            }
            ParcelTracker? result = await TrackFromApi(parcelId).ConfigureAwait(false);

            if (result == null)
            {
                return NotFound();
            }
                
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    private async Task<ParcelTracker?> TrackFromApi(string parcelId)
    {
        ParcelTracker? result = null;
        ParcelTrackerDTO? response;
        string xOkapiKey = _parcelyApiConfiguration.XOkapiKey ?? string.Empty; //SandBox Key for the tests      

        try
        {
            if (string.IsNullOrEmpty(xOkapiKey))
            {
                throw new NullReferenceException();
            }
            HttpClient httpClient = _httpClientFactory.CreateClient(Constants.LaPoste);
            httpClient.DefaultRequestHeaders.Add("X-Okapi-Key", xOkapiKey);
            Uri uri = new($"{httpClient.BaseAddress}{_parcelyApiConfiguration.Url}{_parcelyApiConfiguration.Route}{parcelId}");

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uri).ConfigureAwait(false);

            if (httpResponseMessage is null || !httpResponseMessage.IsSuccessStatusCode || httpResponseMessage.Content is null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, parcelId, xOkapiKey));
            }

            string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!string.IsNullOrEmpty(content))
            {
                response = JsonSerializer.Deserialize<ParcelTrackerDTO?>(content);
                result = FromDTOToDomain(response);
            }
        }
        catch (Exception ex) when (ex is InvalidOperationException || ex is NullReferenceException || ex is UriFormatException)
        {
            _logger.LogError(ex.Message, ex);
        }
        return result;
    }

    private ParcelTracker? FromDTOToDomain(ParcelTrackerDTO? parcelTrackerDTO)
    {

        if (parcelTrackerDTO != null)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ParcelTrackerDTO, ParcelTracker>();    
                cfg.CreateMap<ParcelTrackerDTO.ShipmentDTO, ParcelTracker.ShipmentDomain>();
                cfg.CreateMap<ParcelTrackerDTO.Timeline, ParcelTracker.Timeline>();
                cfg.CreateMap<ParcelTrackerDTO.Event, ParcelTracker.Event>();
                cfg.CreateMap<ParcelTrackerDTO.ContextDataDTO, ParcelTracker.ContextDataDomain>();
                cfg.CreateMap<ParcelTrackerDTO.DeliveryChoiceDTO, ParcelTracker.DeliveryChoiceDomain>();
                cfg.CreateMap<ParcelTrackerDTO.Partner, ParcelTracker.Partner>();
            });

            Mapper mapper = new Mapper(config);
            return mapper.Map<ParcelTracker>(parcelTrackerDTO);
        }
        return new ParcelTracker();
    }
}
