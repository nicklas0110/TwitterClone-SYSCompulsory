using AutoMapper;
using UserService.Core.Entities;
using UserService.Core.Repositories.Interfaces;
using UserService.Core.Services.DTOs;
using UserService.Core.Services.Interfaces;

namespace UserService.Core.Services;

public class UserService : IUserService 
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<GetUserDTO> GetUserById(int userId)
    {
        return _mapper.Map<GetUserDTO>(await _userRepository.GetUserById(userId));
    }

    public async Task CreateUser(CreateUserDTO user)
    {
        if (user != null)
        {
            await _userRepository.AddUser(_mapper.Map<User>(user));
        }
        else throw new ArgumentException("User cannot be null");
    }

    public async Task<PaginatedResult<GetUserDTO>> GetAllUsers(PaginatedDTO dto)
    {
        return _mapper.Map<PaginatedResult<GetUserDTO>>(
            await _userRepository.GetAllUsers(dto.PageNumber, dto.PageSize));
    }

    public async Task DeleteUser(int userId)
    {
        var user = await _userRepository.GetUserById(userId);
        if (user != null)
        {
            await _userRepository.DeleteUser(userId);
        }
        else throw new KeyNotFoundException($"No user with userId: {userId} found");
    }
}