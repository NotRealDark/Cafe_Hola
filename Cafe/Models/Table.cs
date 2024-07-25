using System;
using System.Collections.Generic;

namespace Cafe.Models;

public partial class Table
{
    public int Tid { get; set; }

    public int Size { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<TableOrder> TableOrders { get; set; } = new List<TableOrder>();
}
