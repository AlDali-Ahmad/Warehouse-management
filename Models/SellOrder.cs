using System;
using System.Collections.Generic;

namespace Warehouse_Management_2.Models;

public partial class SellOrder
{
    public int Id { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? Supplierid { get; set; }

    public int? DiscountValue { get; set; }

    public string? DiscountPercentage { get; set; }

    public double? BeforDiscountTotal { get; set; }

    public double? AfterDiscount { get; set; }

    public virtual ICollection<SellOrderDetail> SellOrderDetails { get; } = new List<SellOrderDetail>();

    public virtual Supplier? Supplier { get; set; }
}
