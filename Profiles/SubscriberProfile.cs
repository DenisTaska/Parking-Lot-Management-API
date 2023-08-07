using AutoMapper;

namespace Parking_Lot_Management.API.Profiles;

public class SubscriberProfile : Profile
{
    public SubscriberProfile()
    {
        CreateMap<Entities.Subscriber, Models.SubscriberDto>().ReverseMap();
        CreateMap<Entities.Subscriber, Models.SubscriberForCreationDto>().ReverseMap();
        CreateMap<Entities.Subscriber, Models.SubscriberForUpdateDto>().ReverseMap();
    }
}