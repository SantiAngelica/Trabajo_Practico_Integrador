using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class Game
{
    public int Id { get; set; }

    public int IdUserCreator { get; set; }

    public int MissingPlayers { get; set; }

    public virtual ICollection<GameAplication> GameAplications { get; set; } = new List<GameAplication>();

    public virtual ICollection<GameInvitation> GameInvitations { get; set; } = new List<GameInvitation>();

    public virtual ICollection<GameUser> GameUsers { get; set; } = new List<GameUser>();

    public virtual User IdUserCreatorNavigation { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
