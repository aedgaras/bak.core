using System.ComponentModel;

namespace bak.api.Enums;

public enum Classification
{
    [Description("Veterinarian")] Veterinarian = 0,
    [Description("Specialist")] Specialist = 1,
    [Description("Customer")] Customer = 2
}