using Parking_Lot_Management.API.Entities;
using Parking_Lot_Management.API.Models;

namespace Parking_Lot_Management.API.Services.Interfaces;

public interface ISubscriptionService
{
    Task<List<SubscriptionDto>> GetAllSubscriptions();
    Task<List<SubscriptionDto>> GetAllSubscriptions(string? code, string? subscriberName);
    Task<Subscription> GetSingleSubscription(int id);
    
    Task<List<Subscription>> CreateSubscription(SubscriptionForCreationDto subscriptionForCreationDto);
    
    Task<Subscription?> UpdateSubscription (int id, SubscriptionForUpdateDto request);

    Task<List<Subscription>?> DeleteSubscription(int id);
}