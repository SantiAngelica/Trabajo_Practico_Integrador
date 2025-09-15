using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class PropertyTypeField
{
    public int Id { get; set; }

    public int? FieldType { get; set; }

    public int? IdProperty { get; set; }

    public virtual Property? IdPropertyNavigation { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
