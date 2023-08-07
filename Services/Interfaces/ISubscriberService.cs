using Parking_Lot_Management.API.Entities;
using Parking_Lot_Management.API.Models;

namespace Parking_Lot_Management.API.Services.Interfaces;

public interface ISubscriberService
{
    Task<List<Subscriber>> GetAllSubscribers();
    Task<List<Subscriber>> GetAllSubscribers(string? firstName, string? lastName, string? idCardNumber, string? email);
    Task<Subscriber> GetSingleSubscriber(string idCardNumber);
    Task<List<Subscriber>> CreateSubscriber(SubscriberForCreationDto subscriberForCreationDto);
    Task<Subscriber?> UpdateSubscriber(string idCardNumber, SubscriberForUpdateDto subscriberForUpdate);
    Task<List<Subscriber>?> DeleteSubscriber(string idCardNumber);
}