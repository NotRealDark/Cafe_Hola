using System;
using System.Collections.Generic;

namespace Cafe.Models;

public partial class OrderItem
{
    public int CoffeeOrderId { get; set; }

    public int CoffeeId { get; set; }

    public int? Quantity { get; set; }

    public virtual Coffee Coffee { get; set; } = null!;

    public virtual CoffeeOrder CoffeeOrder { get; set; } = null!;
}
