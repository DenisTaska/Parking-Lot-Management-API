using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parking_Lot_Management.API.DbContexts;
using Parking_Lot_Management.API.Entities;
using Parking_Lot_Management.API.Models;
using Parking_Lot_Management.API.Services.Interfaces;

namespace Parking_Lot_Management.API.Services;

public class ParkingSpotService : IParkingSpotService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ParkingSpotService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ParkingSpot>> GetAllSpots()
    {
        var spots = await _context.ParkingSpots.ToListAsync();
        return spots;
    }

    public async Task<int> GetNumberOfReservedSpots()
    {
        var reservedSpots = await _context.Subscriptions
            .Where(c => c.IsDeleted == false)
            .ToListAsync();
        
        return reservedSpots.Count;
    }
    
    public async Task<int> GetNumberOfRegularSpots()
    {
        var totalSpots = await _context.ParkingSpots.ToListAsync();

        var reservedSpots = await _context.Subscriptions
            .Where(c => c.IsDeleted == false)
            .ToListAsync();

        var regularSpots = totalSpots.Count - reservedSpots.Count;

        return regularSpots;
    }
    
    public async Task<int> GetNumberOfOccupiedRegularSpots()
    {
        var occupiedRegularSpots = await _context.Logs
            .Where(l => l.CheckedOut == false && l.SubscriptionId == null)
            .ToListAsync();

        return occupiedRegularSpots.Count;
    }

    public async Task<int> GetNumberOfFreeRegularSpots()
    {
        return await GetNumberOfRegularSpots() - await GetNumberOfOccupiedRegularSpots();
    }

    public async Task<ParkingSpot> CreateParkingSpot()
    {
        var newParkingSpot = new ParkingSpot();

        _context.ParkingSpots.Add(newParkingSpot);

        await _context.SaveChangesAsync();

        return newParkingSpot;
    }
    
    
    public async Task<List<ParkingSpot>?> DeleteParkingSpot(int id)
    {
        var parkingSpot = await _context.ParkingSpots.FindAsync(id);
        if (parkingSpot is null)
            return null;

        _context.ParkingSpots.Remove(parkingSpot);
        await _context.SaveChangesAsync();

        return await _context.ParkingSpots.ToListAsync();
    }
}