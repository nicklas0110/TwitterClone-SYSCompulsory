using CommentService.Core.Services;
using EasyNetQ;
using Messaging;
using Messaging.Messages;

namespace CommentService.MessageHandlers;

public class DeleteCommentOnTweetIfTweetIsDeletedHandler : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public DeleteCommentOnTweetIfTweetIsDeletedHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    private async void HandleDeleteCommentOnTweetIfTweetIsDeleted(DeleteCommentOnTweetIfTweetIsDeleted deleted)
    {
        Console.WriteLine(deleted.Message);

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var commentService = scope.ServiceProvider.GetRequiredService<ICommentService>();
            await commentService.DeleteCommentsOnTweet(deleted.TweetId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ArgumentException("error");
        }
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Message handler running");

        var messageClient = new MessageClient(
            RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")    
        );

        var topic = "DeleteCommentsOnTweetIfTweetIsDeleted";

        await messageClient.Listen<DeleteCommentOnTweetIfTweetIsDeleted>(HandleDeleteCommentOnTweetIfTweetIsDeleted, topic);
    }
}