using AutoMapper;
using FollowedTweetsService.Core.Services;
using FollowedTweetsService.Core.Entities;


namespace FollowedTweetsService.Configs;

public static class AutoMapperConfig
{
    public static IMapper ConfigureAutoMapper()
    {
        var mapperConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<AddToFollowedTweetsDto, FollowedTweets>();

        });

        return mapperConfig.CreateMapper();
    }
}