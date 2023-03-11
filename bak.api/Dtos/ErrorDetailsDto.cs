using System.Text.Json;

namespace bak.api.Dtos;

public class ErrorDetailsDto
{
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}