using System;
using System.Collections.Generic;

namespace NS.FoodOrder.Data.Entities;

public partial class User
{
    public long Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public long RoleId { get; set; }

    public int Age { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Country { get; set; }

    public string PinCode { get; set; }

    public bool IsVerified { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public string PhoneNo { get; set; }

    public string Email { get; set; }

    public string ProfilePic { get; set; }

    public string Password { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<Cart> Carts { get; } = new List<Cart>();

    public virtual ICollection<OrderReceived> OrderReceiveds { get; } = new List<OrderReceived>();

    public virtual Role Role { get; set; }
}
