using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Memory;

var filePath = Path.Combine(AppContext.BaseDirectory, "products_2.json");
    var json = File.ReadAllText(filePath);
    var products = JsonSerializer.Deserialize<List<Product>>(json);
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();
var app = builder.Build();

app.MapGet("/products", (IMemoryCache cache) =>
{   
    if (!cache.TryGetValue("result", out Total total))
    {
    var accessoriesValue = 0;
    var laptopValue = 0;
    var screenValue = 0;

    foreach (var product in products)
    {
        if (product.Name== "Laptop")
        {
            laptopValue++;
        }
        if (product.Name== "Screen")
        {
            screenValue++;
        }
        else accessoriesValue++;
    }
    total =  new Total(laptopValue,screenValue,accessoriesValue);
    cache.Set("result", total);
    }
    
    return Results.Ok(new {total});
});
app.Run();

public record Product(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("price")] decimal Price);

    public record Total(int LaptopValue,int ScreenValue, int AccessoriesValue);