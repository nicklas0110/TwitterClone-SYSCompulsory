namespace Messaging.Messages;

public class AddTweetToTimelineIfTweetIsCreated
{
    public string Message { get; set; }
    public int TweetId { get; set; }

    public AddTweetToTimelineIfTweetIsCreated(string message, int tweetId)
    {
        Message = message;
        TweetId = tweetId;
    }
}