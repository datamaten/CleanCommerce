using API;
using Persistence;
using Persistence.Context;
using Application;
using API.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApiServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();

app.UseOpenApi();
app.UseSwaggerUi();
app.UseExceptionHandler(options => { });
app.MapEndpoints();

app.Run();

public partial class Program;
