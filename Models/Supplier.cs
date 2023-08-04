using System;
using System.Collections.Generic;

namespace Warehouse_Management_2.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? NumberPhone { get; set; }

    public int? Cityid { get; set; }

    public virtual City? City { get; set; }

    public virtual ICollection<SellOrder> SellOrders { get; } = new List<SellOrder>();
}
