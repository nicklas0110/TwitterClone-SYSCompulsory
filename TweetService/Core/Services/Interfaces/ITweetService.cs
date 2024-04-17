using TweetService.Core.Entities;
using TweetService.Core.Services.DTOs;

namespace TweetService.Core.Services.Interfaces
{
    public interface ITweetService
    {
        public Task<IEnumerable<Tweet>> GetTweets();
        
        public Task<Tweet> GetTweetById(int tweetId);
        
        public Task<Tweet> AddTweet(AddTweetDTO comment);

        public Task DeleteTweet(int tweetId);
    }
}