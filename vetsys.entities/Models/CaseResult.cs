using System.Text.Json.Serialization;

namespace vetsys.entities.Models;

public class CaseResult : Entity
{
    public int UserId { get; set; }
    [JsonIgnore]public User User { get; set; }
    public int CaseId { get; set; }
    [JsonIgnore]public User Case { get; set; }
}