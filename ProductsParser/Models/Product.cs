namespace ProductsParser.Models;

public class Product
{
    public decimal Price { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Manufacturer { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;
}