using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register CORS service
builder.Services.AddCors();

var app = builder.Build();

// Configure CORS
app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api", async (HttpClient httpClient) =>
{
    var url = $"https://g.tenor.com/v1/search?q=excited&key=LIVDSRZULELA&limit=8";
    var response = await httpClient.GetStringAsync(url);

    return response;
})
.WithName("GetExcitedGifs")
.WithOpenApi();

app.Run();
