using System;
using System.Collections.Generic;

namespace Warehouse_Management_2.Models;

public partial class City
{
    public int Id { get; set; }

    public string? CityName { get; set; }

    public virtual ICollection<Customer> Customers { get; } = new List<Customer>();

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();

    public virtual ICollection<Supplier> Suppliers { get; } = new List<Supplier>();
}
