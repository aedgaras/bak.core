using System.ComponentModel;

namespace vetsys.entities.Enums;

public enum CaseStatus
{
    [Description("Filled")] Filled = 0,

    [Description("Ongoing")] Ongoing = 1,

    [Description("Completed")] Completed = 2,

    [Description("Closed")] Closed = 3
}