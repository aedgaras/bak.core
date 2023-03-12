using System.Text.Json.Serialization;

namespace vetsys.entities.Models;

public class ViewHistory : Entity
{
    public int UserId { get; set; }
    [JsonIgnore]public User User { get; set; }
    public List<string> Actions { get; set; }
}