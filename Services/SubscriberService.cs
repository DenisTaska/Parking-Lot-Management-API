using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parking_Lot_Management.API.DbContexts;
using Parking_Lot_Management.API.Entities;
using Parking_Lot_Management.API.Models;
using Parking_Lot_Management.API.Services.Interfaces;

namespace Parking_Lot_Management.API.Services;

public class SubscriberService : ISubscriberService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;


    public SubscriberService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Subscriber>> GetAllSubscribers()
    {
        var subscribers = await _context.Subscribers.ToListAsync();
        return subscribers;
    }

    public async Task<List<Subscriber>> GetAllSubscribers(string? firstName, string? lastName, string? idCardNumber,
        string? email)
    {
        if (!string.IsNullOrEmpty(firstName))
        {
            firstName = firstName.Trim();
            return await _context.Subscribers
                .Where(c => c.FirstName == firstName)
                .OrderBy(c => c.FirstName).ToListAsync();
        }

        if (!string.IsNullOrEmpty(lastName))
        {
            lastName = lastName.Trim();
            return await _context.Subscribers
                .Where(c => c.LastName == lastName)
                .OrderBy(c => c.LastName).ToListAsync();
        }

        if (!string.IsNullOrEmpty(idCardNumber))
        {
            idCardNumber = idCardNumber.Trim();
            return await _context.Subscribers
                .Where(c => c.IdCardNumber == idCardNumber)
                .OrderBy(c => c.IdCardNumber).ToListAsync();
        }

        if (!string.IsNullOrEmpty(email))
        {
            email = email.Trim();
            return await _context.Subscribers
                .Where(c => c.Email == email)
                .OrderBy(c => c.Email).ToListAsync();
        }

        return await GetAllSubscribers();
    }

    public async Task<Subscriber> GetSingleSubscriber(string idCardNumber)
    {
        var subscriber = await _context.Subscribers.FindAsync(idCardNumber);
        if (subscriber is null)
            return null;

        return subscriber;
    }

    public async Task<List<Subscriber>> CreateSubscriber(SubscriberForCreationDto subscriberForCreationDto)
    {
        // Check if a subscriber with the same ID
        if (await _context.Subscribers.AnyAsync(s => s.IdCardNumber == subscriberForCreationDto.IdCardNumber))
        {
            throw new Exception(
                $"The Subscriber with the id card: {subscriberForCreationDto.IdCardNumber} already exists");
        }

        var subscriber = _mapper.Map<Subscriber>(subscriberForCreationDto);

        _context.Subscribers.Add(subscriber);
        await _context.SaveChangesAsync();
        return await _context.Subscribers.ToListAsync();
    }

    public async Task<Subscriber?> UpdateSubscriber(string idCardNumber, SubscriberForUpdateDto subscriberForUpdate)
    {
        var subscriber1 = await _context.Subscribers.FindAsync(idCardNumber);
        if (subscriber1 is null)
            return null;
        
        _mapper.Map(subscriberForUpdate, subscriber1);
        await _context.SaveChangesAsync();
        return subscriber1;
    }

    public async Task<List<Subscriber>?> DeleteSubscriber(string idCardNumber)
    {
        var subscriber = await _context.Subscribers.FindAsync(idCardNumber);
        if (subscriber is null)
            return null;

        subscriber.IsDeleted = true;
        await _context.SaveChangesAsync();

        return await _context.Subscribers.ToListAsync();
    }
}