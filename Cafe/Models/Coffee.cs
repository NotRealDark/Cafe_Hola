using System;
using System.Collections.Generic;

namespace Cafe.Models;

public partial class Coffee
{
    public int Cid { get; set; }

    public string CoffeeName { get; set; } = null!;

    public double Price { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
