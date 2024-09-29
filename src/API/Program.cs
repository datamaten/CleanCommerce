using API;
using Persistence;
using Persistence.Context;
using Application;
using Microsoft.AspNetCore.Mvc;
using API.Middleware;
using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApiServices();

builder.Services.AddProblemDetails();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressMapClientErrors = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();

app.UseOpenApi();
app.UseSwaggerUi();

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseExceptionHandler(_ => { });
app.MapModuleEndpoints();

app.Run();

public partial class Program;
