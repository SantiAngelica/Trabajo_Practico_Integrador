using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class Game
{
    public int Id { get; set; }

    public int IdUserCreator { get; set; }

    public int MissingPlayers { get; set; }

}
