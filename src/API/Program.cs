using API;
using Persistence;
using Persistence.Context;
using Application;
using API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApiServices();

// Tilføj denne linje for at konfigurere ProblemDetails
builder.Services.AddProblemDetails();

// Konfigurer ApiController behavior
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
app.MapEndpoints();

app.Run();

public partial class Program;
