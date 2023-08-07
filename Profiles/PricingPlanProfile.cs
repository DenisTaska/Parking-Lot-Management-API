using AutoMapper;

namespace Parking_Lot_Management.API.Profiles;

public class PricingPlanProfile : Profile
{
    public PricingPlanProfile()
    {
        CreateMap<Entities.PricingPlan, Models.PricingPlanDto>().ReverseMap();
        CreateMap<Entities.PricingPlan, Models.PricingPlanForUpdateDto>().ReverseMap();
    }
}