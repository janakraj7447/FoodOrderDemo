using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NS.FoodOrder.Models
{
    public class Login
    {
    [Required(ErrorMessage = "Email ID is Required")]
    [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Incorrect Email Format")]
    public string Email { get; set; }

  
  [Required(ErrorMessage = "Enter your correct Password")]   
    public string Password { get; set; }

    }
}