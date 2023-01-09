namespace parcelfy.Application.ParcelTrackers.Mappers;

public class ParcelTrackHistoryMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<ParcelTrackHistory, ParcelTracker>()
				.Map(dest => dest.IdShip, src => src.ParcelId)
				.Map(dest => dest.Shipment.Url, src => src.URL)
				.Map(dest => dest.Shipment.Product, src => src.Product)
				.Map(dest => dest.Shipment.IsFinal, src => src.IsFinal)
				.Map(dest => dest.Shipment.IdShip, src => src.ParcelId);
	}
}
