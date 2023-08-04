using System;
using System.Collections.Generic;

namespace Warehouse_Management_2.Models;

public partial class BuyOrderDetail
{
    public int Id { get; set; }

    public int? Productid { get; set; }

    public int? Orderid { get; set; }

    public string? Quentity { get; set; }

    public int? ItemTotal { get; set; }

    public virtual BuyOrder? Order { get; set; }

    public virtual Product? Product { get; set; }
}
