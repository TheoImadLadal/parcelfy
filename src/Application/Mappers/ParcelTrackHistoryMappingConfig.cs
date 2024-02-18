namespace parcelfy.Application.Mappers;
public class ParcelTrackHistoryMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<ParcelTrackerHistoryDTO, ParcelTrackerHistoryEntity>();

		config.NewConfig<ParcelTrackerHistoryEntity, ParcelTrackerDTO>()
				.Map(dest => dest.IdShip, src => src.parcelid)
				.Map(dest => dest.Shipment.Url, src => src.url)
				.Map(dest => dest.Shipment.Product, src => src.product)
				.Map(dest => dest.Shipment.IsFinal, src => src.isfinal)
				.Map(dest => dest.Shipment.IdShip, src => src.parcelid);
	}
}
