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
}
