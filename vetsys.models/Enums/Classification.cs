using System.ComponentModel;

namespace vetsys.models.Enums;

public enum Classification
{
    [Description("Veterinarian")] Veterinarian = 0,
    [Description("Specialist")] Specialist = 1,
    [Description("Customer")] Customer = 2
}