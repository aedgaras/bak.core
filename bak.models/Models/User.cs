using System.ComponentModel.DataAnnotations;
using bak.models.Enums;

namespace bak.models.Models;

public class User : Entity
{
    [Required] [MinLength(4)] public string Username { get; init; }

    [Required] [MinLength(4)] public string Password { get; init; }

    public Role Role { get; set; }
    public Classification Classification { get; set; }
    public List<Case> Cases { get; set; }
    public List<Animal> Animals { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}