using System;
using System.Collections.Generic;

namespace Warehouse_Management_2.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public int? Discount { get; set; }

    public int? CountInStock { get; set; }

    public DateTime? Expir { get; set; }

    public int? SellPrice { get; set; }

    public int? BuyPrice { get; set; }

    public virtual ICollection<BuyOrderDetail> BuyOrderDetails { get; } = new List<BuyOrderDetail>();

    public virtual ICollection<SellOrderDetail> SellOrderDetails { get; } = new List<SellOrderDetail>();
}
