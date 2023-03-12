using System.ComponentModel.DataAnnotations;

namespace vetsys.entities.Dtos;

public class HealthRecordDto
{
    [Required] public int HeartRate { get; set; }
}