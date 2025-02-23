using System;
using System.Collections.Generic;

namespace RPm_Gackan1.Models;

public partial class Cataloge
{
    public int IdCataloge { get; set; }

    public string ProductName { get; set; } = null!;

    public string? ProductDescription { get; set; }

    public decimal ProductPrice { get; set; }

    public string Images { get; set; } = null!;

    public int Quantity { get; set; }

    public int ProductCategoriesId { get; set; }

    public int MagazinesId { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Magazine Magazines { get; set; } = null!;

    public virtual ICollection<PosOrder> PosOrders { get; set; } = new List<PosOrder>();

    public virtual ProductCategory ProductCategories { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
