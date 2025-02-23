using System;
using System.Collections.Generic;

namespace RPm_Gackan1.Models;

public partial class Magazine
{
    public int IdMagazines { get; set; }

    public string NameMagazin { get; set; } = null!;

    public string AddressMagazin { get; set; } = null!;

    public virtual ICollection<Cataloge> Cataloges { get; set; } = new List<Cataloge>();
}
