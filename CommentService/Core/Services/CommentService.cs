using AutoMapper;
using CommentService.Core.Entities;
using CommentService.Core.Helper;
using CommentService.Core.Repositories;
using CommentService.Core.Services;
using Messaging;
using Messaging.Messages;

namespace CommentService.Core.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;
    private readonly MessageClient _messageClient;

    public CommentService(ICommentRepository commentRepository, IMapper mapper, MessageClient messageClient)
    {
        _commentRepository = commentRepository ?? throw new ArgumentException("not null");
        _mapper = mapper ?? throw new ArgumentException("Automapper should not be be null");
        _messageClient = messageClient ?? throw new ArgumentException("MessageClient should not be null");
    }

    public async Task<List<Comment>> GetComments(int tweetId)
    {
        if (tweetId < 1) throw new ArgumentException("id should not be < 1");
        return await _commentRepository.GetAllCommentsForTweet(tweetId);
    }


    public async Task AddComment(AddCommentDto comment, int userIdOfTweet)
    {
        await _commentRepository.AddComment(_mapper.Map<Comment>(comment));
        
        var commentsCount = await _commentRepository.GetCommentsAmountOnTweet(comment.TweetId);
        await _messageClient.Send( new NotifyUserAboutComments($"Comment count: {commentsCount}", commentsCount), $"CommentsOnTweetCreatedBy{comment.UserId}");
    }

    public async Task DeleteComment(int commentId)
    {
        if (commentId < 1) throw new ArgumentException("Id should not be < 1");

        if (!await _commentRepository.DoesCommentExists(commentId))
        {
            throw new KeyNotFoundException($"Cannot find comment: {commentId}");
        }
        await _commentRepository.DeleteComment(commentId);

    }

    public async Task DeleteCommentsOnTweet(int tweetId)
    {
        if (tweetId < 1) throw new ArgumentException("Id cannot be less than 1");
        await _commentRepository.DeleteCommentsOnTweet(tweetId);
    }
}