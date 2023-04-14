using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace NS.FoodOrder.Data.CustomEntities
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email ID is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your correct Password")]
        public string Password { get; set; }

    }
}