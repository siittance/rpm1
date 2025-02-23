using System;
using System.Collections.Generic;

namespace RPm_Gackan1.Models;

public partial class User
{
    public int IdUsers { get; set; }

    public string UserLogin { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
