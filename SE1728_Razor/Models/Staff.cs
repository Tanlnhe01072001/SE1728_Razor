using System;
using System.Collections.Generic;

namespace SE1728_Razor.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
