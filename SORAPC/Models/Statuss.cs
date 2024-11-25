using System;
using System.Collections.Generic;

namespace SORAPC.Models;

public partial class Statuss
{
    public int IdStatus { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
