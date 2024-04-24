using CommentService.Core.Entities;
using CommentService.Core.Helper;

namespace CommentService.Core.Services;

public interface ICommentService
{
   
    public Task<PaginatedResult<Comment>> GetComments(int tweetId, PaginatedDto dto);
    
    public Task AddComment(AddCommentDto comment, int userIdOfTweet);

    public Task DeleteComment(int commentId);

    public Task DeleteCommentsOnTweet(int tweetId);
}