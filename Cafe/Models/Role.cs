using System;
using System.Collections.Generic;

namespace Cafe.Models;

public partial class Role
{
    public int Rid { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
