using FollowedTweetsService.Core.Repositories;
using FollowedTweetsService.Core.Services;
using Microsoft.EntityFrameworkCore;
using FollowedTweetsService.Core.Helper;
using FollowedTweetsService.MessageHandlers;

namespace FollowedTweetsService.Configs;

public static class DependencyInjectionConfig
{
    public static void ConfigureDi(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(options =>
            options.UseInMemoryDatabase("FollowedTweetsDb"));
        services.AddScoped<IFollowedTweetsRepository, FollowedTweetsRepository>();
        services.AddScoped<IFollowedTweetsService, Core.Services.FollowedTweetsService>();
        services.AddSingleton(AutoMapperConfig.ConfigureAutoMapper());
        services.AddHostedService<AddTweetToFollowedTweetsIfTweetIsCreatedHandler>();
    }
}