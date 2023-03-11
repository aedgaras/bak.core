using System.Text.Json.Serialization;

namespace bak.models.Models;

public class HealthRecord : Entity
{
    public int HeartRate { get; set; }

    public int AnimalId { get; set; }
    [JsonIgnore] public Animal Animal { get; set; }
}