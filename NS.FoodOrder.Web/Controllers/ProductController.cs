using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NS.FoodOrder.Data.CustomEntities;
using NS.FoodOrder.Data;
using NS.FoodOrder.Business;
using NS.FoodOrder.Web.Models;
using NS.FoodOrder.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Foodorder.Controllers;

[Authorize]
public class ProductController : Controller
{

    private readonly ILogger<ProductController> _logger;
    public readonly IProductBussiness _iProductBussiness;
    public readonly ICategoryBussiness _iCategoryBussiness;

    private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;
    public ProductController(ILogger<ProductController> logger, Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment, IProductBussiness iProductBussiness, ICategoryBussiness iCategoryBussiness)
    {
        _logger = logger;
        Environment = _environment;
        _iProductBussiness = iProductBussiness;
        _iCategoryBussiness = iCategoryBussiness;

    }


    public IActionResult Products()
    {
        var UserDetail = _iProductBussiness.GetProductList();
        return View(UserDetail);
    }

    [HttpGet]
    public IActionResult AddEditProduct(int id)
    {
        // ViewBag.Categories = new SelectList(_iUserBussiness.GetCategoryList(), "Id", "Name");
        ViewBag.Categories = new SelectList(_iCategoryBussiness.GetCategoryList(), "Id", "Name");
        if (id > 0)
            return View(_iProductBussiness.GetProductById(id));
        else
            return View(new AddEditProductViewModel());
    }

    [HttpPost]
    public IActionResult AddEditProduct(AddEditProductViewModel addEditProductViewModel, IFormFile Photo)
    {
        string wwwPath = this.Environment.WebRootPath;
        string contentPath = this.Environment.ContentRootPath;
        string path = Path.Combine(this.Environment.WebRootPath, "UploadProduct");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        if (addEditProductViewModel.Id == 0)
        {
            List<string> uploadedFiles = new List<string>();
            string fileName = Path.GetFileName(Photo.FileName);
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                Photo.CopyTo(stream);
                uploadedFiles.Add(fileName);
                ViewBag.Message += string.Format("<b>{0}</b> Profile pic uploaded.<br />", fileName);
            }

            addEditProductViewModel.Photo = fileName;
        }


        addEditProductViewModel.CreatedBy = Convert.ToInt64(User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
        _iProductBussiness.AddEditProduct(addEditProductViewModel);
        return RedirectToAction(nameof(Products));
    }
    public IActionResult ActivateDeactivateProduct(int Id)
    {

        _iProductBussiness.ActivateDeactivateProduct(Id);
        return RedirectToAction(actionName: "Products", controllerName: "Product");

    }
    public IActionResult ActivateDeactivateEligible(int Id)
    {

        _iProductBussiness.ActivateDeactivateEligible(Id);
        return RedirectToAction(actionName: "Products", controllerName: "Product");

    }


    [HttpGet]
    public IActionResult Details(int categoryId)
    {
        var productList = _iProductBussiness.GetProductByCategoryId(categoryId);

        return PartialView("_MenuItems", productList);

    }

    public IActionResult ViewCart()
    {
        long userId = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);
        var cartItems = _iProductBussiness.GetCartItems(userId);

        return View(cartItems);
    }

    [HttpGet]
    public IActionResult AddToCart(int id)
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        CartViewModel cartViewModel = new CartViewModel();
        cartViewModel.ProductId = id;
        cartViewModel.UserId = Convert.ToInt64(userId);

        _iProductBussiness.AddToCart(cartViewModel);
        // return View();
        return RedirectToAction(actionName: "ViewCart", controllerName: "Product");


    }
    public IActionResult DeleteItem(int Id)
    {
        _iProductBussiness.DeleteItem(Id);
        return RedirectToAction(actionName: "ViewCart", controllerName: "Product");
    }
}

    // public IActionResult GetCartItems(){
    //     long userId = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);
    //     _iProductBussiness.GetCartItems(userId);
    //     return View();
    // }
