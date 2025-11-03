using System.ComponentModel.DataAnnotations;

namespace ProductStoreApi.Models;
public class ProductReview
{
    public int Id { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    [StringLength(500)]
    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int ProductId { get; set; }
    public virtual Product Product { get; set; } = null!;

    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
}