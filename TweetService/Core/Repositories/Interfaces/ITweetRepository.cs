using TweetService.Core.Entities;

namespace TweetService.Core.Repositories.Interfaces;

public interface ITweetRepository
{
    
    public Task<IEnumerable<Tweet>> GetTweets();

    public Task<Tweet> GetTweetById(int tweetId);
    public Task<Tweet> AddTweet(Tweet tweet);

    public Task DeleteTweet(int tweetId);

    public Task<bool> DoesTweetExists(int tweetId);
}