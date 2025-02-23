using System;
using System.Collections.Generic;

namespace RPm_Gackan1.Models;

public partial class ProductCategory
{
    public int IdProductCategory { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Cataloge> Cataloges { get; set; } = new List<Cataloge>();
}
