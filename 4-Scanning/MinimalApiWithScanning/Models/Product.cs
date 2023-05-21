namespace MinimalApiWithScanning.Models;

public record ProductResponse(int id,string name, decimal price, int stock);


public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}