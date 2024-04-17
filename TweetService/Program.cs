using AutoMapper;
using EasyNetQ;
using Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using TweetService.Core.Entities;
using TweetService.Core.Helper;
using TweetService.Core.Repositories;
using TweetService.Core.Repositories.Interfaces;
using TweetService.Core.Services.DTOs;
using TweetService.Core.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var mapperConfig = new MapperConfiguration(config =>
{
    //DTO to entity
    config.CreateMap<AddTweetDTO, Tweet>();
}).CreateMapper();

builder.Services.AddSingleton(mapperConfig);
builder.Services.AddSingleton(new MessageClient(RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")));

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseInMemoryDatabase("TweetDb"));
builder.Services.AddScoped<ITweetRepository, TweetRepository>();
builder.Services.AddScoped<ITweetService, TweetService.Core.Services.TweetService>();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();