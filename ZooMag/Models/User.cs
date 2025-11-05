using System.ComponentModel.DataAnnotations;

namespace ProductStoreApi.Models;

// В Models/User.cs
public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty; // BCrypt хэш

    // Связи
    public virtual ICollection<DeliveryInfo> DeliveryInfos { get; set; } = new List<DeliveryInfo>();
    public virtual ICollection<ProductReview> Reviews { get; set; } = new List<ProductReview>();
}
