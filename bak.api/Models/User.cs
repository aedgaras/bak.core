using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using bak.api.Enums;

namespace bak.api.Models;

public class User
{
    [JsonIgnore] public int Id { get; set; }

    [Required] [MinLength(4)] public string Username { get; init; }

    [Required] [MinLength(4)] public string Password { get; init; }

    public Role Role { get; set; }
    public List<Case> Cases { get; set; }
}