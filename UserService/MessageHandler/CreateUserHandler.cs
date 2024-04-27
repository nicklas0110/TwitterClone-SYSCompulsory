using EasyNetQ;
using Messaging;
using Messaging.Messages;
using UserService.Core.Services.DTOs;
using UserService.Core.Services.Interfaces;

namespace UserService.MessageHandler;

public class CreateUserHandler : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public CreateUserHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    private async void HandleCreateUser(CreateUser user)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var dto = new CreateUserDTO
            {
                Email = user.Email,
                Username = user.Username,
                Password = user.Password,
                CreatedAt = DateTime.Now,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            await userService.CreateUser(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ArgumentException("An error occured while creating user - MessagingService");
        }
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("CreateUserHandler is up and running");

        var messageClient = new MessageClient(
            RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest"));

        var subject = "CreateUser";
        await messageClient.Listen<CreateUser>(HandleCreateUser, subject);
    }
}