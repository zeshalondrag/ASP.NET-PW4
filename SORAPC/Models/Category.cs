using System;
using System.Collections.Generic;

namespace SORAPC.Models;

public partial class Category
{
    public int IdCategory { get; set; }

    public string Names { get; set; } = null!;

    public virtual ICollection<Catalog> Catalogs { get; set; } = new List<Catalog>();
}
