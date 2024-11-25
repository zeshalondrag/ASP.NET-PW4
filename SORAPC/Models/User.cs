using System;
using System.Collections.Generic;

namespace SORAPC.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string UserSurname { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string UserMiddleName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Logins { get; set; } = null!;

    public string Passwords { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
