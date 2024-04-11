using System.Text.Json; 
// Add this line to import the necessary namespace
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

public class TenorService
{
    private readonly HttpClient _httpClient;

    public TenorService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
}
