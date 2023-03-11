using System.Text.Json.Serialization;
using bak.models.Enums;

namespace bak.models.Models;

public class Animal : Entity
{
    public string Name { get; set; }
    public AnimalType Type { get; set; }

    public int UserId { get; set; }
    [JsonIgnore] public User User { get; set; }
    public List<HealthRecord> HealthRecords { get; set; }
}