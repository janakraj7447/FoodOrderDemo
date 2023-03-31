using System;
using System.Collections.Generic;

namespace NS.FoodOrder.Data.Entities;

public partial class Product
{
    public long Id { get; set; }

    public long CategoryId { get; set; }

    public string Name { get; set; }

    public string Price { get; set; }

    public string Description { get; set; }

    public bool IsEligibleForDiscount { get; set; }

    public string Photo { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? Updateddate { get; set; }

    public virtual ICollection<Cart> Carts { get; } = new List<Cart>();

    public virtual Category Category { get; set; }

    public virtual ICollection<OrderReceived> OrderReceiveds { get; } = new List<OrderReceived>();
}
