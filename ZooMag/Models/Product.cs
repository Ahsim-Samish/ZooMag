using System.ComponentModel.DataAnnotations;

namespace ProductStoreApi.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Required]
    [Range(0.01, 10000)]
    public decimal Price { get; set; }

    [Range(0, 1000)]
    public int Stock { get; set; }

    public virtual ICollection<ProductReview> Reviews { get; set; } = new List<ProductReview>();
}
