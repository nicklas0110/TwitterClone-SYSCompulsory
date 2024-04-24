using FollowedTweetsService.Core.Entities;

namespace FollowedTweetsService.Core.Services;

public interface IFollowedTweetsService
{
    public Task<IEnumerable<FollowedTweets>> GetFollowedTweets();
    
    public Task AddToFollowedTweets(AddToFollowedTweetsDto dto);
    
    public Task DeleteFromFollowedTweets(int tweetId);
}