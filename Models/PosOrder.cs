using System;
using System.Collections.Generic;

namespace RPm_Gackan1.Models;

public partial class PosOrder
{
    public int IdPosOrder { get; set; }

    public int CatalogeId { get; set; }

    public int OrdersId { get; set; }

    public int Countt { get; set; }

    public virtual Cataloge Cataloge { get; set; } = null!;

    public virtual Order Orders { get; set; } = null!;
}
