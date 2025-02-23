using System;
using System.Collections.Generic;

namespace RPm_Gackan1.Models;

public partial class Review
{
    public int IdReview { get; set; }

    public int Rating { get; set; }

    public string? ReviewText { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UsersId { get; set; }

    public int CatalogeId { get; set; }

    public virtual Cataloge Cataloge { get; set; } = null!;

    public virtual User Users { get; set; } = null!;
}
