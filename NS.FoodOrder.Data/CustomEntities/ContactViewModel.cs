using System;
using System.Collections.Generic;

namespace NS.FoodOrder.Data.CustomEntities
{
public partial class ContactViewModel
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Subject { get; set; }

    public string Description { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
}