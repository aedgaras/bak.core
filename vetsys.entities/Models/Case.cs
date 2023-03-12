using System.Text.Json.Serialization;
using vetsys.entities.Enums;

namespace vetsys.entities.Models;

public class Case : Entity
{
    public CaseStatus Status { get; set; }
    public int AnimalId { get; set; }
    [JsonIgnore] public Animal Animal { get; set; }
}