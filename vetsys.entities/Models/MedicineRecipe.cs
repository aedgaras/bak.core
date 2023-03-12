using System.Text.Json.Serialization;

namespace vetsys.entities.Models;

public class MedicineRecipe : Entity
{
    public int UserId { get; set; }
    [JsonIgnore]public User User { get; set; }
    public int CaseId { get; set; }
    [JsonIgnore]public Case Case { get; set; }
    public string Title { get; set; }
    public int Count { get; set; }
    public string Description { get; set; }
}