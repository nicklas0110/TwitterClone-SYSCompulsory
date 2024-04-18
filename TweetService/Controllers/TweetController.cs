using System.Data.Common;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TweetService.Core.Services.DTOs;
using TweetService.Core.Services.Interfaces;

namespace TweetService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TweetController : ControllerBase
{
    private readonly ITweetService _tweetService;
    public TweetController(ITweetService tweetService)
    {
        _tweetService = tweetService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTweets()
    {
        try
        {
            return Ok(await _tweetService.GetTweets());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("{tweetId}")]
    public async Task<IActionResult> GetTweetById([FromRoute] int tweetId)
    {
        try
        {
            return Ok(await _tweetService.GetTweetById(tweetId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("postTweet")]
    public async Task<IActionResult> AddTweet([FromBody] AddTweetDTO dto)
    {
        try
        {
            return StatusCode(201, await _tweetService.AddTweet(dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("{tweetId}")]
    public async Task<IActionResult> DeleteTweet([FromRoute] int tweetId)
    {
        try
        {
            await _tweetService.DeleteTweet(tweetId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}