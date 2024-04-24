using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AuthorisationService.Core.Entities;
using AuthorisationService.Core.Repositories;
using AuthorisationService.Core.Services;
using Messaging;
using Messaging.Messages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace AuthorisationService.Core.Services;

public class AuthorisationService : IAuthorisationService
{
    private readonly IAuthorisationRepository _authorisationRepository;
    private readonly MessageClient _messageClient;


    private const string SecurityKey =
        "mMdAhocQbIAa1/4iD8W5BiDCD9Lxg9ULp4qROgJVN8oRZommyAsnRalnNlzWGbKGJItr/kh2jVd2d9brhSBAJttV7NE47dvyX6n36cFKlnz3k9AodqqVgH/S52oQMYamtI+HsQqBmsvZMqOE+oGlEIzJG9tmDZ1JE/qJHq+bXo3RCEuBf26dGuIG4DWpjh+G4xTVC7ZoByCmq5zTUUyTlFZCQ2483iJe1Thkem9mlzt3cOy8O5SYJBafIb0xdIBYEoHl56Z805fO/W4eAw+M5stSCUdJTBUtWbCiId9zSapmilb20sCg4l5xYTsaJImTfHlo0t9kF1o/RXwr1cw3zCPoyt9tjWhZ83LMsi1ydBg=";


    public AuthorisationService(IAuthorisationRepository authorisationRepository, MessageClient messageClient)
    {
        _authorisationRepository = authorisationRepository;
        _messageClient = messageClient;
    }

    public async Task Register(CreateAuthorisationDto dto)
    {
        var authorisation = await _authorisationRepository.DoesAuthorisationExists(dto.Email);

        if (authorisation) throw new DuplicateNameException("Email already exists");

        var saltBytes = new byte[32];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(saltBytes);


        var salt = Convert.ToBase64String(saltBytes);

        var newAuthorisation = new Authorisation()
        {
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password + salt),
            Salt = salt,
        };

        await _authorisationRepository.Register(newAuthorisation);

        await _messageClient.Send(
            new CreateUser("Creating user", dto.Username, dto.Email, newAuthorisation.PasswordHash, dto.FirstName, dto.LastName), "CreateUser");
    }

    public async Task<AuthenticationToken> Login(LoginDto authorisation)
    {
        var loggedInUser = await _authorisationRepository.GetAuthorisationByEmail(authorisation.Email);
        if (loggedInUser == null) throw new Exception("Wrong login");

        if (BCrypt.Net.BCrypt.Verify(authorisation.Password + loggedInUser.Salt, loggedInUser.PasswordHash))
        {
            return GenerateToken(loggedInUser);
        }

        throw new Exception("Wrong login");
    }

    public async Task<AuthenticateResult> ValidateToken(string token)
    {
        if (token.IsNullOrEmpty()) return await Task.Run(() => AuthenticateResult.Fail("Wrong token"));

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecurityKey);
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            }, out var validatedToken);


            if (principal == null)
            {
                return AuthenticateResult.Fail("Wrong token");
            }

            var claims = new List<Claim>();
            foreach (var claim in principal.Claims)
            {
                claims.Add(new Claim(claim.Type, claim.Value));
            }

            var claimsIdentity = new ClaimsIdentity(claims, "dev");
            var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
            var ticket = new AuthenticationTicket(claimPrincipal, "dev");
            return AuthenticateResult.Success(ticket);
        }
        catch
        {
            return await Task.Run(() => AuthenticateResult.Fail("Wrong token"));
        }
    }


    private AuthenticationToken GenerateToken(Authorisation authorisation)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim("Id", authorisation.Id.ToString()),
            new Claim("Email", authorisation.Email),
        };
        
        var tokenOptions = new JwtSecurityToken(
            signingCredentials: signingCredentials,
            claims: claims,
            expires: DateTime.Now.AddMinutes(5)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        var authorisationToken = new AuthenticationToken
        {
            Value = tokenString
        };

        return authorisationToken;
    }
}