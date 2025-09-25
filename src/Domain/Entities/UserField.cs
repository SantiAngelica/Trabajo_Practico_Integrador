using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class UserField
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int Field { get; set; }

    public virtual User User { get; set; } = null!;
}
