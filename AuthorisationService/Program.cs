using AuthorisationService.Core.Helper;
using AuthorisationService.Core.Repositories;
using AuthorisationService.Core.Services;
using EasyNetQ;
using Messaging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(new MessageClient(RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")));

builder.Services.AddScoped<IAuthorisationRepository, AuthorisationRepository>();
builder.Services.AddScoped<IAuthorisationService, AuthorisationService.Core.Services.AuthorisationService>();
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseInMemoryDatabase("AuthDb"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();