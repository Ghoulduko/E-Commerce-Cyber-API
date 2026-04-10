using Cyber.Core.Enums;

namespace Cyber.Application.Dtos.Shipping;

public class ShippingDto
{
    public int Id { get; init; }
    public List<ShippingItemDto> ShippingItems { get; init; }
    public ShippingStatus ShippingStatus { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public decimal Cost { get; init; }
    public int UserId { get; init; }
    public int ShippingAddressId { get; init; }
}