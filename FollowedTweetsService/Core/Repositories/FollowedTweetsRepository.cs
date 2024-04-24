using FollowedTweetsService.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using FollowedTweetsService.Core.Entities;
using FollowedTweetsService.Core.Helper;

namespace FollowedTweetsService.Core.Repositories;

public class FollowedTweetsRepository : IFollowedTweetsRepository
{
    private readonly DatabaseContext _context;

    public FollowedTweetsRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FollowedTweets>> GetFollowedTweets()
    {
        return await _context.FollowedTweets.ToListAsync();
    }

    public async Task AddToFollowedTweets(FollowedTweets followedTweets)
    {
        await _context.FollowedTweets.AddAsync(followedTweets);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteFromFollowedTweets(int tweetId)
    {
        var tweetsToDelete = await _context.FollowedTweets.FirstOrDefaultAsync(p => p.TweetId == tweetId);

        if (tweetsToDelete == null) throw new KeyNotFoundException($"No tweet of {tweetId}");
        
        _context.FollowedTweets.Remove(tweetsToDelete);
        await _context.SaveChangesAsync();
    }
}