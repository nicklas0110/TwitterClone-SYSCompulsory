using UserService.Core.Services.DTOs;

namespace UserService.Core.Services.Interfaces;

public interface IUserService
{
    public Task<GetUserDTO> GetUserById(int userId);
    public Task CreateUser(CreateUserDTO user);
    public Task DeleteUser(int userId);
}