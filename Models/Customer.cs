using System;
using System.Collections.Generic;

namespace Warehouse_Management_2.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? NumperPhone { get; set; }

    public int? Cityid { get; set; }

    public virtual ICollection<BuyOrder> BuyOrders { get; } = new List<BuyOrder>();

    public virtual City? City { get; set; }
}
