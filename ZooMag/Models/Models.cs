using System.ComponentModel.DataAnnotations;

namespace PetStoreApi.Models;

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
public class DeliveryInfo
{
    public int Id { get; set; }

    [Required]
    public string Address { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string City { get; set; } = string.Empty;

    [Required]
    [StringLength(10)]
    public string PostalCode { get; set; } = string.Empty;

    [Phone]
    public string? Phone { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
}
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
public class Person
{
    public string Name { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public int Age { get; set; } = 0;

}