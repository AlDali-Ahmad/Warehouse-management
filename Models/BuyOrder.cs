using System;
using System.Collections.Generic;

namespace Warehouse_Management_2.Models;

public partial class BuyOrder
{
    public int Id { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? BeforDiscountTotal { get; set; }

    public int? AfterDiscount { get; set; }

    public int? DiscountPercent { get; set; }

    public int? DiscountValue { get; set; }

    public int? Customerid { get; set; }

    public int? Employeeid { get; set; }

    public virtual ICollection<BuyOrderDetail> BuyOrderDetails { get; } = new List<BuyOrderDetail>();

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }
}
