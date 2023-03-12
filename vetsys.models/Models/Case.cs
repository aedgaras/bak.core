using System.Text.Json.Serialization;
using vetsys.models.Enums;

namespace vetsys.models.Models;

public class Case : Entity
{
    public CaseStatus Status { get; set; }
    public int UserId { get; set; }
    [JsonIgnore] public User User { get; set; }
}