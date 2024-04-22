using CommentService.Core.Entities;
using CommentService.Core.Helper;

namespace CommentService.Core.Repositories;

public interface ICommentRepository
{
    
    public Task<PaginatedResult<Comment>> GetComments(int tweetId, int pageNumber, int pageSize);
    
    public Task AddComment(Comment comment);
    
    public Task UpdateComment(int commentId, Comment comment);
    
    public Task DeleteComment(int commentId);
    
    public Task DeleteCommentsOnTweet(int tweetId);
    
    public Task<bool> DoesCommentExists(int commentId);
    
    public Task<int> GetCommentsAmountOnTweet(int tweetId);
}