namespace Cyber.Application.Dtos.Shipping;

public class ShippingHistoryDto
{
    public int Id { get; set; }
    public int ShippingId { get; init; }
    public string Status { get; init; }
    public DateTime ChangedStatusAt { get; init; }
}