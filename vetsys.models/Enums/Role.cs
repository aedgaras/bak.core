using System.ComponentModel;

namespace vetsys.models.Enums;

public enum Role
{
    [Description("User")] User = 0,

    [Description("Admin")] Admin = 1
}