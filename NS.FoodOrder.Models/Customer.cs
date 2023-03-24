using System;
using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
// using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace NS.FoodOrder.Models;

public partial class Customer
{
    [Key]
    public long Id { get; set; }

    [Required(ErrorMessage ="First Name is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage ="Last Name is required")]
    public string LastName { get; set; }

    public long RoleId { get; set; }
    

     [Range(1,120, ErrorMessage ="Age must be between 1-120 in years.")]  
    public int Age { get; set; }

    public string Address { get; set; }
   [Required]
    public string City { get; set; }
    [Required]
    public string State { get; set; }
    [Required]
    public string Country { get; set; }
    [Required]
    public string PinCode { get; set; }

    public bool? IsVerified { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }
    [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter Valid Mobile Number.")]
    public string PhoneNo { get; set; }

      [EmailAddress]
      [Display(Name = "Email")]
    [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,6}", ErrorMessage = "Incorrect Email Format")]
    public string Email { get; set; }
   
    public string ProfilePic { get; set; }
   
     [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
    [Required(ErrorMessage = "Password is required")]   
    public string Password { get; set; }

    [Required(ErrorMessage = "Confirm Password is required")]    
    [DataType(DataType.Password)]    
    [Compare("Password")]   
    public string ConfirmPassword { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    // public virtual ICollection<Cart> Carts { get; } = new List<Cart>();

    // public virtual ICollection<OrderReceived> OrderReceiveds { get; } = new List<OrderReceived>();

    // public virtual Role Role { get; set; }
}

