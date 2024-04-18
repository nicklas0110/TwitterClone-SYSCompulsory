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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

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
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}


app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();