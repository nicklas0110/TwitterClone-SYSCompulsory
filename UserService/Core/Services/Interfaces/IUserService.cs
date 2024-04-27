using UserService.Core.Entities;
using UserService.Core.Services.DTOs;

namespace UserService.Core.Services.Interfaces;

public interface IUserService
{
    public Task<GetUserDTO> GetUserById(int userId);
    public Task CreateUser(CreateUserDTO user);
    public Task<PaginatedResult<GetUserDTO>> GetAllUsers(PaginatedDTO dto);
    public Task DeleteUser(int userId);
}