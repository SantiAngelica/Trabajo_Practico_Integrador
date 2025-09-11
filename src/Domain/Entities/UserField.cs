using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class UserField
{
    public int Id { get; set; }

    public int? Field { get; set; }

    public int? UserId { get; set; }
}
