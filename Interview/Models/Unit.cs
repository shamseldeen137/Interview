using System;
using System.Collections.Generic;

namespace Interview.Models;

public partial class Unit
{
    public int UnitNo { get; set; }

    public string? UnitName { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
}
