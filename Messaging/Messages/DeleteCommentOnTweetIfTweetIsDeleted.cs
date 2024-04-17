namespace Messaging.Messages;

public class DeleteCommentOnTweetIfTweetIsDeleted
{
    public string Message { get; set; }
    public int TweetId { get; set; }

    public DeleteCommentOnTweetIfTweetIsDeleted(string message, int tweetId)
    {
        Message = message;
        TweetId = tweetId;
    }
}