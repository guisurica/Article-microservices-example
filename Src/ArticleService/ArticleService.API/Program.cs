using ArticleService.Application.Contracts.Usecases;
using ArticleService.Application.DTOs;
using ArticleService.Domain.Extensions;
using ArticleService.Infra.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfra(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/create-article", async (
    [FromBody] CreateArticleDTO input,
    IUseCase<ArticleDTO, CreateArticleDTO> createArticleUseCase) =>
{
    var articleCreated = await createArticleUseCase.HandleAsync(input);
    if (!articleCreated.IsOkResult)
        return Results.BadRequest(articleCreated.Message);

    return Results.Ok(articleCreated.Data);
})
.WithName("create-article")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
