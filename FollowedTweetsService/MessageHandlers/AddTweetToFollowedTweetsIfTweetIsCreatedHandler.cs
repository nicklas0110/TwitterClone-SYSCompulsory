using EasyNetQ;
using Messaging;
using Messaging.Messages;
using FollowedTweetsService.Core.Services;

namespace FollowedTweetsService.MessageHandlers;

public class AddTweetToFollowedTweetsIfTweetIsCreatedHandler : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public AddTweetToFollowedTweetsIfTweetIsCreatedHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    private async void HandleAddTweetToFollowedTweetsIfTweetIsCreated(AddTweetToTimelineIfTweetIsCreated tweet)
    {
        Console.WriteLine(tweet.Message);

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var followedTweetsService = scope.ServiceProvider.GetRequiredService<IFollowedTweetsService>();
            var dto = new AddToFollowedTweetsDto
            {
                TweetId = tweet.TweetId,
            };
            await followedTweetsService.AddToFollowedTweets(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ArgumentException("Something went wrong");
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Message handler running..");

        var messageClient = new MessageClient(
            RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")    
        );

        const string topic = "AddTweetToFollowedTweetsIfTweetIsCreated";

        await messageClient.Listen<AddTweetToTimelineIfTweetIsCreated>(HandleAddTweetToFollowedTweetsIfTweetIsCreated, topic);
    }
}