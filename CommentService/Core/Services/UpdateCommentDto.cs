namespace CommentService.Core.Services;

public class UpdateCommentDto
{
    public int Id { get; set; }
    public string Body { get; set; }
    public DateTime? UpdatedAt { get; set; }
}