using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class UserField
{
    public int Id { get; set; }

    public string? Field { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
