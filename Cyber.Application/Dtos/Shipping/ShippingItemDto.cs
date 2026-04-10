namespace Cyber.Application.Dtos.Shipping;

public class ShippingItemDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int ShippingId { get; set; }
    public int ProductId { get; set; }
}