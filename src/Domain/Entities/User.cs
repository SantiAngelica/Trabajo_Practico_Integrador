using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Age { get; set; }

    public string Rol { get; set; } = null!;

    public string Zone { get; set; } = null!;

    public virtual ICollection<GameAplication> GameAplications { get; set; } = new List<GameAplication>();

    public virtual ICollection<GameInvitation> GameInvitations { get; set; } = new List<GameInvitation>();

    public virtual ICollection<GameUser> GameUsers { get; set; } = new List<GameUser>();

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();

    public virtual ICollection<UserComent> UserComents { get; set; } = new List<UserComent>();

    public virtual ICollection<UserField> UserFields { get; set; } = new List<UserField>();

    public virtual ICollection<UserPosition> UserPositions { get; set; } = new List<UserPosition>();
}
