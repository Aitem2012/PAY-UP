using Hangfire;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using PAY_UP.Persistence.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddDatabaseServices(config);
builder.Services.AddApplicationServices();

builder.Services.AddAuthenticationServices(config);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Debt Recovery API",
        TermsOfService = new Uri("https://wwww.pay-up.com/terms-of-service"),
        License = new OpenApiLicense
        {
            Name = "PAY-UP License",
            Url = new Uri("https://www.pay-up.com/license")
        },
        Contact = new OpenApiContact
        {
            Email = "info@pay-up.com",
            Name = "PAY-UP Team",
            Url = new Uri("https://www.pay-up.com"),
        },
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = HeaderNames.Authorization,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseHangfireDashboard();

app.MapControllers();

app.Run();
