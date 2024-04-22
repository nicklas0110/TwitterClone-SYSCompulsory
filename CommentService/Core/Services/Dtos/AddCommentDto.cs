namespace CommentService.Core.Entities.Dtos;

public class AddCommentDto
{
    public int UserId { get; set; }
    public int TweetId { get; set; }
    public string Body { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}