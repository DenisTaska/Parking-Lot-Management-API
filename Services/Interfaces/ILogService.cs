using Parking_Lot_Management.API.Entities;
using Parking_Lot_Management.API.Models;

namespace Parking_Lot_Management.API.Services.Interfaces;

public interface ILogService
{
    Task<List<LogDto>> GetAllLogs();
    Task<List<LogDto>> GetAllLogs(string? code, string? subscriberName);
    Task<List<Log>> GetAllLogsForDay(DateTime date);
    Task<List<LogForResponseDto>> CreateLog(LogForCreationDto logForCreationDto);
    Task<List<Log>?> CheckOutLog(int id);
}