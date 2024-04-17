using AutoMapper;
using Messaging;
using Messaging.Messages;
using TweetService.Core.Entities;
using TweetService.Core.Repositories.Interfaces;
using TweetService.Core.Services.DTOs;
using TweetService.Core.Services.Interfaces;

namespace TweetService.Core.Services
{
    public class TweetService : ITweetService
    {
        private readonly ITweetRepository _tweetRepository;
        private readonly IMapper _mapper;
        private readonly MessageClient _messageClient;

        public TweetService(ITweetRepository tweetRepository, IMapper mapper, MessageClient messageClient)
        {
            _tweetRepository = tweetRepository ?? throw new ArgumentException("Tweet repository cannot be null");
            _mapper = mapper ?? throw new ArgumentException("Automapper cannot be null");
            _messageClient = messageClient ?? throw new ArgumentException("MessageClient cannot be null");
        }

        public async Task<IEnumerable<Tweet>> GetTweets()
        {
            return await _tweetRepository.GetTweets();
        }

        public async Task<Tweet> GetTweetById(int tweetId)
        {
            if (tweetId < 1) throw new ArgumentException("Tweet ID cannot be 0 or null");

            return await _tweetRepository.GetTweetById(tweetId);
        }

        public async Task<Tweet> AddTweet(AddTweetDTO dto)
        {
            if (dto.UserId <= 0) throw new ArgumentException("User ID cannot be 0 or null");
            
            var tweet = await _tweetRepository.AddTweet(_mapper.Map<Tweet>(dto));
            await _messageClient.Send(new AddTweetToTimelineIfTweetIsCreated("Adding tweet to timeline", tweet.Id), "AddTweetToTimelineIfTweetIsCreated");
            return tweet;
        }

        public async Task DeleteTweet(int tweetId)
        {
            if (tweetId < 1) throw new ArgumentException("Id cannot be less than 1");

            if (!await _tweetRepository.DoesTweetExists(tweetId))
            {
                throw new KeyNotFoundException($"No such tweet with id of {tweetId}");
            }
            await _tweetRepository.DeleteTweet(tweetId);
            await _messageClient.Send(new DeleteCommentOnTweetIfTweetIsDeleted("Deleting comments on tweet", tweetId), "DeleteCommentsOnTweetIfTweetIsDeleted");

        }
    }
}
