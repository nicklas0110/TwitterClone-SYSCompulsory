FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FollowedTweetsService/FollowedTweetsService.csproj", "FollowedTweetsService/"]
RUN dotnet restore "FollowedTweetsService/FollowedTweetsService.csproj"
COPY . .
WORKDIR "/src/FollowedTweetsService"
RUN dotnet build "FollowedTweetsService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FollowedTweetsService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FollowedTweetsService.dll"]
