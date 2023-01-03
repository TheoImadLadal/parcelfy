namespace parcelfy.Infrastructure.ParcelTrackersRepository.Dtos;

public class ParcelTrackerDto
{
    [JsonPropertyName("lang")]
    public string? Lang { get; set; }
    
    [JsonPropertyName("scope")]
    public string? Scope { get; set; }

    [JsonPropertyName("returnCode")]
    public int? ReturnCode { get; set; }

    [JsonPropertyName("shipment")]
    public ShipmentDto? Shipment { get; set; }

    public class ShipmentDto
    {
        [JsonPropertyName("idShip")]
        public string? IdShip { get; set; }

        [JsonPropertyName("holder")]
        public int? Holder { get; set; }

        [JsonPropertyName("product")]
        public string? Product { get; set; }

        [JsonPropertyName("isFinal")]
        public bool? IsFinal { get; set; }

        [JsonPropertyName("timeline")]
        public List<Timeline>? Timeline { get; set; }

        [JsonPropertyName("event")]
        public List<Event>? Event { get; set; }

        [JsonPropertyName("contextData")]
        public ContextDataDto? ContextData { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }

    public class Timeline
    {

        [JsonPropertyName("shortLabel")]
        public string? ShortLabel { get; set; }

        [JsonPropertyName("longLabel")]
        public string? LongLabel { get; set; }

        [JsonPropertyName("id")]
        public int? Id { get; set; }
        
        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("status")]
        public bool? Status { get; set; }

        [JsonPropertyName("type")]
        public int? Type { get; set; }
    }

    public class Event
    {

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("label")]
        public string? Label { get; set; }

        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }
    }

    public class ContextDataDto
    {

        [JsonPropertyName("deliveryChoice")]
        public DeliveryChoiceDto? DeliveryChoice { get; set; }

        [JsonPropertyName("partner")]
        public Partner? Partner { get; set; }

        [JsonPropertyName("originCountry")]
        public string? OriginCountry { get; set; }

        [JsonPropertyName("arrivalCountry")]
        public string? ArrivalCountry { get; set; }
    }

    public class DeliveryChoiceDto
    {
        [JsonPropertyName("deliveryChoice")]
        public int? DeliveryChoice { get; set; }
    }

    public class Partner
    {
        [JsonPropertyName("reference")]
        public string? Reference { get; set; }
    }
}
