using Microsoft.EntityFrameworkCore;
using UserService.Core.Entities;
using UserService.Core.Repositories.Interfaces;
using UserService.Core.Services;
using UserService.Core.Services.DTOs;

namespace UserService.Core.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _context;

    public UserRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserById(int userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"No userId matches input: {userId}");
        }

        return user;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            throw new KeyNotFoundException($"No user matches input: {email}");
        }

        return user;
    }

    public async Task<PaginatedResult<User>> GetAllUsers(int pageNumber, int pageSize)
    {
        var users = await _context.Users
            .Skip(pageSize * pageNumber)
            .Take(pageSize)
            .ToListAsync();

        var totalCount = await _context.Users.CountAsync();

        return new PaginatedResult<User>
        {
            Items = users,
            TotalCount = totalCount
        };
    }

    public async Task AddUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUser(int userId)
    {
        var userToBeDeleted = await _context.Users.FindAsync(userId);
        _context.Users.Remove(userToBeDeleted);
        await _context.SaveChangesAsync();
    }
}