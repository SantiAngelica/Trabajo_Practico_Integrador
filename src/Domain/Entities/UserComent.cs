using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class UserComent
{
    public int Id { get; set; }

    public int IdUserCommenter { get; set; }

    public string Body { get; set; } = null!;

    public int UserId { get; set; }

}
