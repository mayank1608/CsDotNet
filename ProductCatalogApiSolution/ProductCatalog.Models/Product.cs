using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCatalog.Models;

public class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public bool IsActive { get; set; } = true;

    // Foreign key
    public int CategoryId { get; set; }

    // Navigation property
    // Each product belongs to one category
    public Category? Category { get; set; }
}