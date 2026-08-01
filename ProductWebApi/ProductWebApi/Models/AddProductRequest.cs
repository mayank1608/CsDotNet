namespace ProductWebApi.Models;
public class AddProductRequest
{
    public string ProductName { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string? Description { get; set; } = string.Empty;

    public int CategoryId { get; set; }
}