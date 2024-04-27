using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TweetService.Controllers;
using TweetService.Core.Entities;
using TweetService.Core.Services.DTOs;
using TweetService.Core.Services.Interfaces;
using Xunit;

namespace TweetService.test;

public class TweetServiceTest
{
    [Fact]  
    public async Task TestCreateTweet_ReturnsOkAndTweet()  
    {  
        // Arrange    
        var testTweet = new Tweet  
        {  
            Id = 1,  
            UserId = 1,  
            Body = "Hello Everybody! This is a test!",  
            CreatedAt = DateTime.UtcNow,  
        };  
        
        var dtoTweet = new AddTweetDTO  
        {  
            UserId = 1,  
            Body = "Test tweet 1 2 3",  
        };
  
        var mockService = new Mock<ITweetService>();  
        mockService.Setup(service => service.AddTweet(It.IsAny<AddTweetDTO>()))  
            .ReturnsAsync(testTweet);
  
        var claims = new List<Claim> { new Claim("UserId", testTweet.UserId.ToString()) };  
        var identity = new ClaimsIdentity(claims, "Bearer");  
        var user = new ClaimsPrincipal(identity);  
  
        var controller = new TweetController(mockService.Object);  
  
        // Act    
        var result = await controller.AddTweet(dtoTweet);  
  
        // Assert    
        var objectResult = Assert.IsType<ObjectResult>(result);
        var tweetResult = Assert.IsType<Tweet>(objectResult.Value);
        Assert.Equal(201, objectResult.StatusCode);
        Assert.Equal(testTweet, tweetResult);
    }

    [Fact]  
    public async Task TestGetTweets_ReturnsListOfTweets()  
    {  
        // Arrange  
        var tweets = new List<Tweet>  
        {  
            new Tweet  
            {  
                Id = 1,  
                UserId = 1,  
                Body = "Test tweet",  
                CreatedAt = DateTime.UtcNow,  
            },  
            new Tweet  
            {  
                Id = 2,  
                UserId = 2,  
                Body = "Test tweeeeet",  
                CreatedAt = DateTime.UtcNow,  
            },  
        };  
  
        var mockService = new Mock<ITweetService>();  
        mockService.Setup(service => service.GetTweets()).ReturnsAsync(tweets);  
  
        var controller = new TweetController(mockService.Object);  
  
        // Act  
        var result = await controller.GetTweets();  
  
        // Assert  
        var okResult = Assert.IsType<OkObjectResult>(result);  
        var returnedTweets = Assert.IsType<List<Tweet>>(okResult.Value);  
        Assert.Equal(tweets.Count, returnedTweets.Count);  
        for (int i = 0; i < tweets.Count; i++)  
        {  
            Assert.Equal(tweets[i].Id, returnedTweets[i].Id);  
            Assert.Equal(tweets[i].UserId, returnedTweets[i].UserId);  
            Assert.Equal(tweets[i].Body, returnedTweets[i].Body);  
        }  
    }  

    
    [Fact]  
    public async Task TestGetTweetById_ReturnsTweet()  
    {  
        // Arrange  
        var tweetId = 1;  
        var tweet = new Tweet  
        {  
            Id = tweetId,  
            UserId = 1,  
            Body = "Test tweet",  
            CreatedAt = DateTime.UtcNow,  
        };  
  
        var mockService = new Mock<ITweetService>();  
        mockService.Setup(service => service.GetTweetById(tweetId)).ReturnsAsync(tweet);  
  
        var controller = new TweetController(mockService.Object);  
  
        // Act  
        var result = await controller.GetTweetById(tweetId);  
  
        // Assert  
        var okResult = Assert.IsType<OkObjectResult>(result);  
        var returnedTweet = Assert.IsType<Tweet>(okResult.Value);  
        Assert.Equal(tweet.Id, returnedTweet.Id);  
        Assert.Equal(tweet.UserId, returnedTweet.UserId);  
        Assert.Equal(tweet.Body, returnedTweet.Body);  
    }  
    
    
    [Fact]  
    public async Task TestDeleteTweet_ReturnsOk()  
    {  
        // Arrange    
        var tweetId = 1;
        
        var mockService = new Mock<ITweetService>();  
        mockService.Setup(service => service.DeleteTweet(tweetId)).Returns(Task.CompletedTask);
        
        var claims = new List<Claim> { new Claim("UserId", tweetId.ToString()) };  
        var identity = new ClaimsIdentity(claims, "Bearer");  
        var user = new ClaimsPrincipal(identity);  
  
        var controller = new TweetController(mockService.Object);  
  
        // Act    
        var result = await controller.DeleteTweet(tweetId);  
  
        // Assert    
        var objectResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, objectResult.StatusCode);
    }  

}