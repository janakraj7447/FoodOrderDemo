using System;
using System.Collections.Generic;

namespace NS.FoodOrder.Data.Entities;

public partial class Category
{
    public long Id { get; set; }

    public string Name { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
