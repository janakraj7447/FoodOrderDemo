using System;
using System.Collections.Generic;

namespace NS.FoodOrder.Data.Entities;

public partial class PaymentMode
{
    public long Id { get; set; }

    public long Name { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? Updateddate { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
}
