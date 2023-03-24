using System;
using System.Collections.Generic;

namespace NS.FoodOrder.Data.Entities;

public partial class Cart
{
    public long Id { get; set; }

    public long ProductId { get; set; }

    public long UserId { get; set; }

    public int Quantity { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual Product Product { get; set; }

    public virtual User User { get; set; }
}
