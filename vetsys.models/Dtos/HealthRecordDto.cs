using System.ComponentModel.DataAnnotations;

namespace vetsys.models.Dtos;

public class HealthRecordDto
{
    [Required] public int HeartRate { get; set; }
}