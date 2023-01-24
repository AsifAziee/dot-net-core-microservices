﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edstem.Services.OrderAPI.Models;

public class OrderDetails
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderDetailsId { get; set; }

    public int OrderHeaderId { get; set; }
    [ForeignKey("OrderHeaderId")] public virtual OrderHeader OrderHeader { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    public string ProductName { get; set; }
    public double Price { get; set; }
}