using System.Text.Json.Serialization;
using bak.api.Enums;

namespace bak.api.Models;

public class Case
{
    public int Id { get; set; }
    public CaseStatus Status { get; set; }
    public int UserId { get; set; }
    [JsonIgnore] public User User { get; set; }
}