using Microsoft.EntityFrameworkCore;
using TweetService.Core.Entities;
using TweetService.Core.Helper;
using TweetService.Core.Repositories.Interfaces;

namespace TweetService.Core.Repositories;

public class TweetRepository : ITweetRepository
{
    private readonly DatabaseContext _context;

    public TweetRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tweet>> GetTweets()
    {
        return await _context.Tweets.ToListAsync();
    }

    public async Task<Tweet> GetTweetById(int tweetId)
    {
        var tweet = await _context.Tweets.FirstOrDefaultAsync(p => p.Id == tweetId);

        if (tweet == null)
        {
            throw new KeyNotFoundException($"No tweet with the id of {tweetId}");
        }

        return tweet;
    }

    public async Task<Tweet> AddTweet(Tweet tweet)
    {
        await _context.Tweets.AddAsync(tweet);
        await _context.SaveChangesAsync();
        return tweet;
    }

    public async Task DeleteTweet(int tweetId)
    {
        var tweetToDelete = await _context.Tweets.FirstOrDefaultAsync(p => p.Id == tweetId);
        _context.Tweets.Remove(tweetToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesTweetExists(int tweetId)
    {
        var tweet = await _context.Tweets.FirstOrDefaultAsync(p => p.Id == tweetId);
        if (tweet != null || tweet != default)
        {
            return true;
        }

        return false;
    }
    
}