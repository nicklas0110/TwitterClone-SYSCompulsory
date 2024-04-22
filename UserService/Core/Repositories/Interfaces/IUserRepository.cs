using UserService.Core.Entities;

namespace UserService.Core.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<User> GetUserById(int userId);
    public Task<User> GetUserByEmail(string email);
    public Task AddUser(User user);
    public Task DeleteUser(int userId);
}