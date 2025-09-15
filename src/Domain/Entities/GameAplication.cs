using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class GameAplication
{
    public int Id { get; set; }

    public int IdUserApplicant { get; set; }

    public int IdGame { get; set; }

    public string State { get; set; } = null!;

    public virtual Game IdGameNavigation { get; set; } = null!;

    public virtual User IdUserApplicantNavigation { get; set; } = null!;
}
