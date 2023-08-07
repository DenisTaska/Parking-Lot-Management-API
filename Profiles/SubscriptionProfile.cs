using AutoMapper;
using Parking_Lot_Management.API.Entities;

namespace Parking_Lot_Management.API.Profiles;

public class SubscriptionProfile : Profile
{
    public SubscriptionProfile()
    {
        CreateMap<Entities.Subscription, Models.SubscriptionDto>().ReverseMap();
        CreateMap<Entities.Subscription, Models.SubscriptionForCreationDto>().ReverseMap();
        CreateMap<Entities.Subscription, Models.SubscriptionForUpdateDto>().ReverseMap();
    }
}