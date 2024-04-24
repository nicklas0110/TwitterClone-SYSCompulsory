using FollowedTweetsService.Core.Entities;

namespace FollowedTweetsService.Core.Repositories;

public interface IFollowedTweetsRepository
{
    public Task<IEnumerable<FollowedTweets>> GetFollowedTweets();
    
    public Task AddToFollowedTweets(FollowedTweets followedTweets);
    
    public Task DeleteFromFollowedTweets(int tweetId);
}