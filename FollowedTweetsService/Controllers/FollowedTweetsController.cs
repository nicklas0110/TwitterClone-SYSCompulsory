using FollowedTweetsService.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FollowedTweetsService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FollowedTweetsController : ControllerBase
{
    private readonly IFollowedTweetsService _followedTweetsService;

    public FollowedTweetsController(IFollowedTweetsService followedTweetsService)
    {
        _followedTweetsService = followedTweetsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetFollowedTweets()
    {
        try
        {
            return Ok(await _followedTweetsService.GetFollowedTweets());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddToFollowedTweets([FromBody] AddToFollowedTweetsDto dto)
    {
        try
        {
            await _followedTweetsService.AddToFollowedTweets(dto);
            return StatusCode(201, "Tweet added to the FollowedTweets");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("{tweetId}")]
    public async Task<IActionResult> DeleteTweetFromFollowedTweets([FromRoute] int tweetId)
    {
        try
        {
            await _followedTweetsService.DeleteFromFollowedTweets(tweetId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}