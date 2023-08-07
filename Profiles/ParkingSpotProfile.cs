using AutoMapper;

namespace Parking_Lot_Management.API.Profiles;

public class ParkingSpotProfile : Profile
{
    public ParkingSpotProfile()
    {
        CreateMap<Entities.ParkingSpot, Models.ParkingSpotDto>().ReverseMap();
        CreateMap<Entities.ParkingSpot, Models.ParkingSpotForCreation>().ReverseMap();
    }
}
