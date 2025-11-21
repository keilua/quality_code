using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/products", (IMemoryCache cache) =>
{
    var filePath = Path.Combine(AppContext.BaseDirectory, "products_2.json");
    var json = File.ReadAllText(filePath);
    var products = JsonSerializer.Deserialize<List<Product>>(json);
    var accessoriesValue = products.Where(product => product.Name != "Laptop" && product.Name != "Screen").ToList()
        .Sum(product => product.Price);
    var laptopValue = products.Where(product => product.Name == "Laptop").ToList().Sum(product => product.Price);
    var screenValue = products.Where(product => product.Name == "Screen").ToList().Sum(product => product.Price);
    Thread.Sleep(20);
    return Results.Ok(new {accessoriesValue, laptopValue, ScreenValue = screenValue});
});
app.Run();

public record Product(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("price")] decimal Price);