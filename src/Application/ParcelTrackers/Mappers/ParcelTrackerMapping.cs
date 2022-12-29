using AutoMapper;
using parcelfy.Application.ParcelTrackers.Models;
using parcelfy.Infrastructure.ParcelTrackersRepository.Dtos;

namespace parcelfy.Application.ParcelTrackers.Mappers;
public static class ParcelTrackerMapping
{
    public static ParcelTracker? FromDTOToDomain(ParcelTrackerDTO? parcelTrackerDTO)
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
