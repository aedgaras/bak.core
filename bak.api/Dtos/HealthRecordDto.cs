using System.ComponentModel.DataAnnotations;

namespace bak.api.Dtos;

public class HealthRecordDto
{
    [Required] public int HeartRate { get; set; }
}