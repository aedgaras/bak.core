﻿using System.ComponentModel;

namespace bak.api.Enums;

public enum Role
{
    [Description("User")]
    User = 0,

    [Description("Admin")]
    Admin = 1
}