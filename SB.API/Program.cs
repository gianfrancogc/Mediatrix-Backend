using Microsoft.OpenApi.Models;
using SB.Application.Services;
using SB.Domain.Interfaces;
using SB.Infrastructure.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IGovernmentEntityRepository, FileGovernmentEntityRepository>();
builder.Services.AddScoped<GovernmentEntityService>();

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration.WriteTo.Console();
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Government Entities API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Government Entities API v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
