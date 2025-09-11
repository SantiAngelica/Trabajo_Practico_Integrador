using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class GameInvitation
{
    public int Id { get; set; }

    public int IdUserReciever { get; set; }

    public int IdGame { get; set; }

    public string State { get; set; } = null!;
}
