using EasyNetQ;
using Messaging;
using Messaging.Messages;

namespace UserService.Core.Helper.MessageHandlers;

public class NotifyUserAboutCommentsHandler : BackgroundService
{
    private readonly int _userId;

    public NotifyUserAboutCommentsHandler(int userId)
    {
        _userId = userId;
    }
    
    private void HandleNotifyUserAboutComments(NotifyUserAboutComments notifyUserAboutComments)
    {
        Console.WriteLine(notifyUserAboutComments.Message);
        Console.WriteLine($"Total comments count: {notifyUserAboutComments.CommentsCount}");
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Message handler is running..");

        var messageClient = new MessageClient(
            RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")    
        );

        var topic = $"CommentsOnPostCreatedByUserId_{_userId}";
        await messageClient.Listen<NotifyUserAboutComments>(HandleNotifyUserAboutComments, topic);
    }
}