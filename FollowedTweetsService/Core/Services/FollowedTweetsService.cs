using AutoMapper;
using FollowedTweetsService.Core.Repositories;
using FollowedTweetsService.Core.Services;
using FollowedTweetsService.Core.Entities;

namespace FollowedTweetsService.Core.Services;

public class FollowedTweetsService : IFollowedTweetsService
{
    private readonly IFollowedTweetsRepository _followedTweetsRepository;
    private readonly IMapper _mapper;

    public FollowedTweetsService(IFollowedTweetsRepository followedTweetsRepository, IMapper mapper)
    {
        _followedTweetsRepository = followedTweetsRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FollowedTweets>> GetFollowedTweets()
    {
        return await _followedTweetsRepository.GetFollowedTweets();
    }

    public async Task AddToFollowedTweets(AddToFollowedTweetsDto dto)
    {
        if (dto.TweetId <= 0) throw new ArgumentException("Tweet id cannot be 0 or < 0");

        await _followedTweetsRepository.AddToFollowedTweets(_mapper.Map<FollowedTweets>(dto));
    }

    public async Task DeleteFromFollowedTweets(int tweetId)
    {
        if (tweetId <= 0) throw new ArgumentException("Tweet id cannot be 0 or < 0");
        await _followedTweetsRepository.DeleteFromFollowedTweets(tweetId);
    }
}