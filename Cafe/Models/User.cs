using System;
using System.Collections.Generic;

namespace Cafe.Models;

public partial class User
{
    public int Uid { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public int RoleId { get; set; }

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<CoffeeOrder> CoffeeOrders { get; set; } = new List<CoffeeOrder>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<TableOrder> TableOrders { get; set; } = new List<TableOrder>();
}
