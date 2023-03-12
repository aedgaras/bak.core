using System.ComponentModel.DataAnnotations;

namespace vetsys.models.Dtos;

public class UserDto
{
    public int Id { get; set; }
    [Required] [MinLength(4)] public string Username { get; set; }
    [Required] [MinLength(4)] public string Password { get; set; }
    public string Role { get; set; }
    public string Classification { get; set; }
}