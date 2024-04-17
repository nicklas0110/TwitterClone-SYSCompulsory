namespace TweetService.Core.Services.DTOs;

public class AddTweetDTO
{
    public int? TweetId { get; set; }
    public int UserId { get; set; }
    public string Body { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow; 
}