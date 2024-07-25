using System;
using System.Collections.Generic;

namespace Cafe.Models;

public partial class TableOrder
{
    public int Toid { get; set; }

    public int? BookerId { get; set; }

    public int TableId { get; set; }

    public DateTime BookTime { get; set; }

    public virtual User? Booker { get; set; }

    public virtual ICollection<CoffeeOrder> CoffeeOrders { get; set; } = new List<CoffeeOrder>();

    public virtual Table Table { get; set; } = null!;
}
