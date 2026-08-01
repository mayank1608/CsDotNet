namespace ProductWebApi.Models;
public class ProductResponse
{
    public int? ProductId { get; set; }

    public string? ProductName { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public int CategoryId { get; set; }

    public string? CategoryName { get; set; } = string.Empty;
}