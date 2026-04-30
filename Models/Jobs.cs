using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtAuthAPI.Models;

[Table("Jobs")]
public class Job
{
    [Key]
    public int JobId { get; set; }

    public int ShootId { get; set; }

    public int PhotographerId { get; set; }

    public DateTime ShootDate { get; set; }

    public string EventType { get; set; } = "";

    public string ShootCategory { get; set; } = "";

    public string SaleType { get; set; } = "";

    public string Status { get; set; } = "";

    public int CreatedBy { get; set; }


    [ForeignKey("CreatedBy")]
    public User CreatedByUser
    { get; set; } = null!;


    [ForeignKey("PhotographerId")]
    public User Photographer
    { get; set; } = null!;
}