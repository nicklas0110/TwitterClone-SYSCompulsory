using System.Text.Json;
using CommentService.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommentService.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly HttpClient _client = new HttpClient();

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    [Route("{tweetId}")]
    public async Task<IActionResult> GetComments([FromRoute] int tweetId)
    {
        try
        {
            var comments = await _commentService.GetComments(tweetId);
            return Ok(comments);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("PostComment")]
    public async Task<IActionResult> AddComment([FromBody] AddCommentDto dto)
    {
        try
        {
            var request = await _client.GetAsync($"http://TweetService/api/Tweet/{dto.TweetId}");

            if (!request.IsSuccessStatusCode) throw new KeyNotFoundException($"No tweet id of {dto.TweetId}");
            var result = await request.Content.ReadAsStringAsync();
            JsonDocument doc = JsonDocument.Parse(result);
            JsonElement root = doc.RootElement;
            int userId = root.GetProperty("userId").GetInt32();
            
            await _commentService.AddComment(dto, userId);
            return StatusCode(201, "Comment success");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("{commentId}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
    {
        try
        {
            await _commentService.DeleteComment(commentId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("DeleteCommentsOnTweet/{tweetId}")]
    public async Task<IActionResult> DeleteCommentsOnTweet([FromRoute] int tweetId)
    {
        try
        {
            await _commentService.DeleteCommentsOnTweet(tweetId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}