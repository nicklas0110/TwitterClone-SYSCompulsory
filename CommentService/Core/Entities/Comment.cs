﻿namespace CommentService.Core.Entities;

public class Comment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TweetId { get; set; }
    public string Body { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}