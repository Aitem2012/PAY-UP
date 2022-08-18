using FluentValidation.AspNetCore;
using PAY_UP.Application.Validators.SmS;
using PAY_UP.Persistence.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.

builder.Services.AddDatabaseServices(config);
builder.Services.AddApplicationServices();
builder.Services.AddControllers()
    .AddFluentValidation(opt =>
    {
        opt.RegisterValidatorsFromAssembly(typeof(SmSDtoValidator).GetTypeInfo().Assembly);
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
