using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NS.FoodOrder.Data.CustomEntities;
using NS.FoodOrder.Data;
using NS.FoodOrder.Business;
using NS.FoodOrder.Web.Models;
using NS.FoodOrder.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Foodorder.Controllers;
// 
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public readonly IUserBussiness _iUserBussiness;

    private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;
    public HomeController(ILogger<HomeController> logger, Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment, IUserBussiness iUserBussiness)
    {
        _logger = logger;
        Environment = _environment;
        _iUserBussiness = iUserBussiness;
    }

    public IActionResult Index()
    {
        return View();
    }
    [Authorize(Roles = "1")]
    public IActionResult Privacy(string Sorting_Order, string Search_Data)
    {
        ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Name_Description" : "";
        // ViewBag.SortingDate = Sorting_Order == "Date_Enroll" ? "Date_Description" : "Date";
        var UserDetail = _iUserBussiness.GetUserList( ViewBag.SortingName,  ViewBag.SortingDate);
        return View(UserDetail);

    }

    public IActionResult About()
    {
        return View();
    }
    public IActionResult Menu()
    {
        return View();
    }
    [Authorize(Roles = "2")]
    public IActionResult Contact()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddUser(Customer customer, IFormFile ProfilePic)
    {
        string wwwPath = this.Environment.WebRootPath;
        string contentPath = this.Environment.ContentRootPath;
        string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        List<string> uploadedFiles = new List<string>();
        string fileName = Path.GetFileName(ProfilePic.FileName);
        using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
        {
            ProfilePic.CopyTo(stream);
            uploadedFiles.Add(fileName);
            ViewBag.Message += string.Format("<b>{0}</b> Profile pic uploaded.<br />", fileName);
        }
        customer.Password = BCrypt.Net.BCrypt.HashPassword(customer.Password);
        customer.ProfilePic = fileName;
        _iUserBussiness.AddUser(customer);
        return RedirectToAction(actionName: "Index", controllerName: "Login");

    }
    [HttpPost]
    public IActionResult AddContactDetails(ContactViewModel contactViewModel)
    {
        _iUserBussiness.AddContactDetails(contactViewModel);
        return RedirectToAction(actionName: "Contact", controllerName: "Home");
    }

    public IActionResult DeleteRecord(int Id)
    {

        _iUserBussiness.DeleteRecord(Id);
        return RedirectToAction(actionName: "Privacy", controllerName: "Home");

    }

    public IActionResult Login()
    {
        return View();
    }

    [AcceptVerbs("GET", "POST")]
    public IActionResult VerifyEmail(string email)
    {

        if (_iUserBussiness.VerifyEmail(email) == true)
        {
            return Json($"Email {email} is already in use.");
        }

        return Json(true);
    }


}

//     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//     public IActionResult Error()
//     {
//         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//     }

