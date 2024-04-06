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

app.MapGet("/gif/{id}", async (HttpContext context, string id) =>
{
    // Obtener una instancia de HttpClient a través de la inyección de dependencias
    var httpClient = context.RequestServices.GetRequiredService<HttpClient>();
    // Construir la URL de la API de Tenor con el identificador proporcionado
    var tenorUrl = $"https://g.tenor.com/v1/gifs?ids={id}&key=LIVDSRZULELA&media_filter=gif";
    // Hacer una solicitud HTTP a la API de Tenor
    var response = await httpClient.GetAsync(tenorUrl);
    // Asegurarse de que la respuesta tenga un código de estado de éxito
    response.EnsureSuccessStatusCode();
    // Leer la respuesta como una cadena JSON
    var responseBody = await response.Content.ReadAsStringAsync();
    // Devolver la respuesta de la API de Tenor
    return responseBody;
})
.WithName("TenorSearch")
.WithOpenApi();

var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast(
        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        Random.Shared.Next(-20, 55),
        summaries[Random.Shared.Next(summaries.Length)]
    ))
    .ToArray();

    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}