using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Interview.Models;

public partial class InvoiceDetail
{
    public int LineNumber { get; set; }
    [Required]
    public string? ProductName { get; set; }

    public int? UnitNo { get; set; }
    [Required]

    public int? Price { get; set; }

    public int? Quantity { get; set; }

    public int? Total { get; set; }
    [Required,DataType(DataType.Date)]

    public DateTime? ExpiryDate { get; set; }

    public virtual Unit? UnitNoNavigation { get; set; }
}
