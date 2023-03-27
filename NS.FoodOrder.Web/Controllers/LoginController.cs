using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
// using Foodorder.Entities;
using NS.FoodOrder.Data.CustomEntities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NS.FoodOrder.Business;
using NS.FoodOrder.Data;

namespace Foodorder.Controllers
{
  
    public class Login : Controller
    {
        private readonly ILogger<Login> _logger;
        public readonly IUserBussiness _iUserBussiness;
        public Login(ILogger<Login> logger,IUserBussiness iUserBussiness)
        {
            _logger = logger;
            _iUserBussiness=iUserBussiness;
        }

        public IActionResult Index()
        {
            return View();
        }

      [HttpPost]
        public IActionResult LoginPage(Customer customer){
      
            var userDetails=_iUserBussiness.LoginPage(customer);
            if(userDetails!=null && BCrypt.Net.BCrypt.Verify(customer.Password,userDetails.Password)){

                var claims=new Claim[]{new Claim(ClaimTypes.Email,userDetails.Email),new Claim(ClaimTypes.Role,userDetails.Password)};
                var identity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(identity));
                return RedirectToAction("Privacy","Home");
            }
            else{
                ViewData["Errormsg"]="Incorrect Email or Password";
                return View("Index");
            }
        
        return View();
    }

    public IActionResult Logout(){
        HttpContext.SignOutAsync();
        return RedirectToAction("Index","Login");
    }
         
   
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}