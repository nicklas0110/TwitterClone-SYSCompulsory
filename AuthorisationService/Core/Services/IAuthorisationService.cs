using Microsoft.AspNetCore.Authentication;

namespace AuthorisationService.Core.Services;

public interface IAuthorisationService
{
    public Task Register(CreateAuthorisationDto authorisation);
    public Task<AuthenticationToken> Login(LoginDto user);
    public Task<AuthenticateResult> ValidateToken(string token);
}