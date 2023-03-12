using System.Text.Json.Serialization;

namespace vetsys.entities.Models;

public class HealthRecord : Entity
{
    public int HeartRate { get; set; }

    public int AnimalId { get; set; }
    [JsonIgnore] public Animal Animal { get; set; }
}