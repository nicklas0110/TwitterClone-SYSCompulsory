namespace TweetService.Core.Entities;

public class Tweet
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Body { get; set; }
    public DateTime CreatedAt { get; set; }
    
}