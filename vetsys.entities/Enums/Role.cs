using System.ComponentModel;

namespace vetsys.entities.Enums;

public enum Role
{
    [Description("User")] User = 0,

    [Description("Admin")] Admin = 1
}