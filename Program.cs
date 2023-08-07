using Microsoft.EntityFrameworkCore;
using Parking_Lot_Management.API.DbContexts;
using Parking_Lot_Management.API.Entities;
using Parking_Lot_Management.API.Services;
using Parking_Lot_Management.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IParkingSpotService, ParkingSpotService>();
builder.Services.AddScoped<IPricingPlanService, PricingPlanService>();
builder.Services.AddScoped<ISubscriberService, SubscriberService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddDbContext<DataContext>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();