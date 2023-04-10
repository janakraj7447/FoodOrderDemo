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

[Authorize]
public class CategoryController : Controller
{

    private readonly ILogger<CategoryController> _logger;
    public readonly ICategoryBussiness _iCategoryBussiness;

    private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;
    public CategoryController(ILogger<CategoryController> logger, Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment, ICategoryBussiness iCategoryBussiness)
    {
        _logger = logger;
        Environment = _environment;
        _iCategoryBussiness = iCategoryBussiness;

    }
     public IActionResult Categories()
    {
        var UserDetail = _iCategoryBussiness.GetCategoryList();
        return View(UserDetail);

    }

       [HttpGet]
    public IActionResult AddEditCategory(int id)
    {
        if (id > 0)
            return View(_iCategoryBussiness.GetCategoryById(id));
        else
            return View();
    }

    [HttpPost]
    public IActionResult AddEditCategory(AddEditCategoryViewModel addEditCategoryViewModel)
    {
        addEditCategoryViewModel.CreatedBy = Convert.ToInt64(User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
        _iCategoryBussiness.AddEditCategory(addEditCategoryViewModel);
        return RedirectToAction(nameof(Categories));
    }

     public IActionResult ActivateDeactivateCategory(int Id)
    {

        _iCategoryBussiness.ActivateDeactivateCategory(Id);
        return RedirectToAction(actionName: "Categories", controllerName: "Category");

    }


}