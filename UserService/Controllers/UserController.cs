using Microsoft.AspNetCore.Mvc;
using UserService.Core.Services.DTOs;
using UserService.Core.Services.Interfaces;

namespace UserService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("AddUser")]
    public async Task<IActionResult> AddUser([FromBody] CreateUserDTO dto)
    {
        try
        {
            await _userService.CreateUser(dto);
            return StatusCode(201, "Successfully added user to DB");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //This method is used to get all users from the database
    [HttpGet]
    [Route("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers([FromQuery] PaginatedDTO dto)
    {
        try
        {
            return Ok(await _userService.GetAllUsers(dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }   
    
    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> GetUserById([FromRoute] int userId)
    {
        try
        {
            return Ok(await _userService.GetUserById(userId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("{userId}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int userId)
    {
        try
        {
            await _userService.DeleteUser(userId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}