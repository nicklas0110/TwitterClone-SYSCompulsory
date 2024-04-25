using CommentService.Core.Entities;
using CommentService.Core.Helper;

namespace CommentService.Core.Repositories;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllCommentsForTweet(int tweetId);
    Task AddComment(Comment comment);
    Task UpdateComment(int commentId, Comment comment);
    Task DeleteComment(int commentId);
    Task DeleteCommentsOnTweet(int tweetId);
    Task<bool> DoesCommentExists(int commentId);
    Task<int> GetCommentsAmountOnTweet(int tweetId);
}