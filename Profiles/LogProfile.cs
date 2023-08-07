using AutoMapper;

namespace Parking_Lot_Management.API.Profiles;

public class LogProfile : Profile
{
    public LogProfile()
    {
        CreateMap<Entities.Log, Models.LogDto>().ReverseMap();
        CreateMap<Entities.Log, Models.LogForCreationDto>().ReverseMap();
    }
}