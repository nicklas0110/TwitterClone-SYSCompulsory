using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using AuthorisationService.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthorisationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorisationController : ControllerBase
{
    private readonly IAuthorisationService _authorisationService;

    public AuthorisationController(IAuthorisationService authorisationService)
    {
        _authorisationService = authorisationService;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] CreateAuthorisationDto dto)
    {
        try
        {
            await _authorisationService.Register(dto);
            return StatusCode(201, "Successfully registered");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            return Ok(await _authorisationService.Login(dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("ValidateToken")]
    public async Task<bool> ValidateToken()
    {
        try
        {
            var re = Request;

            if (!re.Headers.ContainsKey("Authorization"))
                return await Task.Run(() => false);

            if (!re.Headers["Authorization"].ToString().StartsWith("Bearer "))
                return await Task.Run(() => false);

            
            var token = re.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _authorisationService.ValidateToken(token);
            return result.Succeeded;
        }
        catch (Exception e)
        {
            return await Task.Run(() => false);
        }
    }
}