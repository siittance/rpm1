using System;
using System.Collections.Generic;

namespace RPm_Gackan1.Models;

public partial class Cart
{
    public int IdCart { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public string UsersId { get; set; } = null!;

    public int CatalogeId { get; set; }

    public virtual Cataloge Cataloge { get; set; } = null!;
}
