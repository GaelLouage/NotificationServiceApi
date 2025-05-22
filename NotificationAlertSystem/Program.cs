using Infrastructuur.Bootstrapper;
using Infrastructuur.Dtos;
using Infrastructuur.Enums;
using Infrastructuur.Factories.NotificationFactory.Classes;
using Infrastructuur.Factories.NotificationFactory.Interfaces;
using Infrastructuur.Helpers;
using Infrastructuur.Models;
using Infrastructuur.Repositories.Classes;
using Infrastructuur.Repositories.Interfaces;
using Infrastructuur.Services.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Core.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// appsettings data
builder.Services.Configure<OptionsSettings>(
    builder.Configuration.GetSection("OptionsSettings")
    );

builder.Services.AddService();

// add console for logging 
builder.Logging.AddConsole(); 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/notification-email", async Task<IResult> (
    ILogger<Program> Logger,
    INotificationService<EmailReciever> notificationService, 
    NotificationEntity<EmailReciever> notification) =>
{
    try
    {
        if(notification.Message is null)
        {
            return Results.BadRequest("Body cannot be empty");
        }
        var(IsValid, Message) = EmailValidationHelper.IsEmailValid(notification.Message.ToEmail);
        if(IsValid is false)
        {
            return Results.BadRequest(Message);
        }

        notification.Type = NotificationType.EMAIL;
        var result = await notificationService.SendAsync(notification);
        return Results.Ok(result.Result);
    }
    catch (Exception ex)
    {
        Logger.LogError(ex, "An error of type {ExceptionType} occurred: {ErrorMessage}", ex.GetType().FullName, ex.Message);
        return Results.Problem("An error occurred while sending email notification.");
    }

})
.WithName("notification-email")
.WithOpenApi();


app.MapPost("/notification-sms", async Task<IResult> (
    ILogger<Program> Logger,
    INotificationService <SmsReciever> notificationService, 
    NotificationEntity<SmsReciever> notification) =>
{
    try
    {
        if (notification.Message is null)
        {
            return Results.BadRequest("Body cannot be empty");
        }
        var phone = notification.Message.ToPhone;
        if (string.IsNullOrEmpty(notification.Message.ToPhone) || phone.Any(x => !char.IsDigit(x)))
        {
            return Results.BadRequest("All numbers of phone has to be digital.");
        }
        notification.Type = NotificationType.SMS;
        var result = await notificationService.SendAsync(notification);
        Logger.LogError(result.Result);
        return Results.Ok(result.Result);
    }
    catch (Exception  ex)
    {
        Logger.LogError(ex, "An error of type {ExceptionType} occurred: {ErrorMessage}", ex.GetType().FullName, ex.Message);
        return Results.Problem("An error occurred while sending sms notification.");
    }
})
.WithName("notification-sms")
.WithOpenApi();


app.Run();
