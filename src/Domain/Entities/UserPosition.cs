using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class UserPosition
{
    public int Id { get; set; }

    public string? Position { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
