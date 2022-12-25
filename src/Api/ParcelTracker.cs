namespace Api;

public class ParcelTracker
{
    public string? Lang { get; set; }
    public string? Scope { get; set; }
    public int? ReturnCode { get; set; }
    public ShipmentDomain? Shipment{ get; set; }

    public class ShipmentDomain
    {
        public string? IdShip { get; set; }
        public int? Holder { get; set; }
        public string? Product { get; set; }
        public bool? IsFinal { get; set; }
        public List<Timeline>? Timeline { get; set; }
        public List<Event>? Event { get; set; }
        public ContextDataDomain? ContextData { get; set; }
        public string? Url { get; set; }
    }

    public class Timeline
    {
        public string? ShortLabel { get; set; }
        public string? LongLabel { get; set; }
        public int? Id { get; set; }
        public string? Country { get; set; }
        public bool? Status { get; set; }
        public int? Type { get; set; }
    }

    public class Event
    {
        public string? Code { get; set; }
        public string? Label { get; set; }
        public DateTime? Date { get; set; }
    }

    public class ContextDataDomain
    {
        public DeliveryChoiceDomain? DeliveryChoice { get; set; }
        public Partner? Partner { get; set; }
        public string? OriginCountry { get; set; }
        public string? ArrivalCountry { get; set; }
    }

    public class DeliveryChoiceDomain
    {
        public int? DeliveryChoice { get; set; }
    }

    public class Partner
    {
        public string? Reference { get; set; }
    }
}
