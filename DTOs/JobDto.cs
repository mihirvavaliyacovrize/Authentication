namespace JwtAuthAPI.DTOs;

public class JobDto
{
    public int ShootId { get; set; }

    public int PhotographerId { get; set; }

    public DateTime ShootDate { get; set; }

    public string EventType { get; set; } = "";

    public string ShootCategory { get; set; } = "";

    public string SaleType { get; set; } = "";

    public string Status { get; set; } = "";
}