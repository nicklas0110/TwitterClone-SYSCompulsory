using AutoMapper;
using UserService.Core.Entities;
using UserService.Core.Services;
using UserService.Core.Services.DTOs;

namespace UserService.Configs;

public static class AutoMapperConfig
{
    public static IMapper ConfigureAutoMapper()
    {
        var mapperConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<CreateUserDTO, User>();
            
            config.CreateMap<PaginatedResult<User>, PaginatedResult<GetUserDTO>>();
            config.CreateMap<User, GetUserDTO>();
        });

        return mapperConfig.CreateMapper();
    }
}