using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace NS.FoodOrder.Data.CustomEntities{

public class CategoryViewModel
{
    public long Id { get; set; }

    public string Name { get; set; }

    public bool? IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

}
}