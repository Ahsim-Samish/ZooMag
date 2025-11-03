using System.ComponentModel.DataAnnotations;

namespace ProductStoreApi.Models;

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
