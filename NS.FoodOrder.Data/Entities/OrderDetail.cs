using System;
using System.Collections.Generic;

namespace NS.FoodOrder.Data.Entities;

public partial class OrderDetail
{
    public long Id { get; set; }

    public string BillValue { get; set; }

    public long PaymentModeId { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<OrderReceived> OrderReceiveds { get; } = new List<OrderReceived>();

    public virtual PaymentMode PaymentMode { get; set; }
}
