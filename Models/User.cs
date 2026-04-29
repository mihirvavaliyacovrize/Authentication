
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtAuthAPI.Models;

[Table("AppUsers")]
public class User
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string Email { get; set; } = "";

    public string PasswordHash { get; set; } = "";

    public List<Job> JobsCreated
    { get; set; } = new();
}