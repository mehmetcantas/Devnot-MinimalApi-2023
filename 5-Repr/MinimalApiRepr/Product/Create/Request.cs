namespace MinimalApiRepr.Product.Create;

public class Request
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public Entity.Product ToEntity()
    {
        return new()
        {
            Id = Id,
            Name = Name,
            Price = Price,
            Stock = Stock
        };
    }
}