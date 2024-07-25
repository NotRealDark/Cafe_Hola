using System;
using System.Collections.Generic;

namespace Cafe.Models;

public partial class CoffeeOrder
{
    public int Coid { get; set; }

    public int? TableOrderId { get; set; }

    public int? SellerId { get; set; }

    public double? TotalPrice { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User? Seller { get; set; }

    public virtual TableOrder? TableOrder { get; set; }
}
