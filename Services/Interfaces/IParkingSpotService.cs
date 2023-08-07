using Parking_Lot_Management.API.Entities;
using Parking_Lot_Management.API.Models;

namespace Parking_Lot_Management.API.Services.Interfaces;

public interface IParkingSpotService
{
    Task<List<ParkingSpot>> GetAllSpots();
    Task<int> GetNumberOfReservedSpots();
    Task<int> GetNumberOfRegularSpots();
    Task<int> GetNumberOfFreeRegularSpots();
    Task<int> GetNumberOfOccupiedRegularSpots();
    Task<ParkingSpot> CreateParkingSpot();
    Task<List<ParkingSpot>?> DeleteParkingSpot(int id);
}