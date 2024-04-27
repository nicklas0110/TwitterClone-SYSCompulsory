using UserService.Core.Entities;
using UserService.Core.Services.DTOs;

namespace UserService.Core.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<User> GetUserById(int userId);
    public Task<User> GetUserByEmail(string email);
    public Task<PaginatedResult<User>> GetAllUsers(int pageNumber, int pageSize);
    public Task AddUser(User user);
    public Task DeleteUser(int userId);
}