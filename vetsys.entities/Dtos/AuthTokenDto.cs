namespace vetsys.entities.Dtos;

public class AuthTokenDto
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}