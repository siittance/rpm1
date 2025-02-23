using System;
using System.Collections.Generic;

namespace RPm_Gackan1.Models;

public partial class Order
{
    public int IdOrders { get; set; }

    public int OrderNumber { get; set; }

    public decimal SumBill { get; set; }

    public DateTime DateOrder { get; set; }

    public int UsersId { get; set; }

    public virtual ICollection<PosOrder> PosOrders { get; set; } = new List<PosOrder>();

    public virtual User Users { get; set; } = null!;
}
