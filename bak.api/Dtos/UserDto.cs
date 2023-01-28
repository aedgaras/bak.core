using System.ComponentModel.DataAnnotations;
using bak.api.Models;

namespace bak.api.Dtos;

public class UserDto
{
    [Required, MinLength(4)]
    public string Username { get; set; }
    [Required, MinLength(4)]
    public string Password { get; set; }
    public Role Role { get; set; }
}