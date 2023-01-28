using System.ComponentModel.DataAnnotations;

namespace bak.api.Dtos;

public class AuthDto
{
    [Required] [MinLength(4)] public string Username { get; set; }

    [Required] [MinLength(4)] public string Password { get; set; }
}