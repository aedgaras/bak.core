using System.ComponentModel.DataAnnotations;

namespace bak.models.Dtos;

public class HealthRecordDto
{
    [Required] public int HeartRate { get; set; }
}