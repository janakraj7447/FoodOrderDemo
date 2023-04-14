using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NS.FoodOrder.Data.CustomEntities;
using NS.FoodOrder.Data;
using NS.FoodOrder.Business;
using NS.FoodOrder.Web.Models;
using NS.FoodOrder.Data.Entities;
using PayPal.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace Foodorder.Controllers;
public class CartController : Controller
{
    private IHttpContextAccessor httpContextAccessor;
    IConfiguration _configuration;
    private readonly ILogger<ProductController> _logger;
    public readonly IProductBussiness _iProductBussiness;
    public readonly ICategoryBussiness _iCategoryBussiness;
    public readonly ICartBussiness _iCartBussiness;

    private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;
    public CartController(ILogger<ProductController> logger, Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment, IProductBussiness iProductBussiness, ICategoryBussiness iCategoryBussiness, ICartBussiness iCartBussiness, IHttpContextAccessor Context, IConfiguration iconfiguration)
    {
        _logger = logger;
        Environment = _environment;
        _iProductBussiness = iProductBussiness;
        _iCategoryBussiness = iCategoryBussiness;
        _iCartBussiness = iCartBussiness;
        httpContextAccessor = Context;
        _configuration = iconfiguration;

    }
    public IActionResult ViewCart()
    {
        long userId = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);
        var cartItems = _iCartBussiness.GetCartItems(userId);

        return View(cartItems);
    }

    [HttpGet]
    public IActionResult AddToCart(int id)
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        CartViewModel cartViewModel = new CartViewModel();
        cartViewModel.ProductId = id;
        cartViewModel.UserId = Convert.ToInt64(userId);

        _iCartBussiness.AddToCart(cartViewModel);
        // return View();
        return RedirectToAction(actionName: "ViewCart", controllerName: "Cart");


    }
    [HttpGet]
    public bool AddQuantity(int productId)
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        return _iCartBussiness.AddQuantity(productId, Convert.ToInt64(userId));
    }
    [HttpGet]
    public bool SubtractQuantity(int productId)
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        return _iCartBussiness.SubtractQuantity(productId, Convert.ToInt64(userId));
    }
    public IActionResult DeleteItem(long Id)
    {
        _iCartBussiness.DeleteItem(Id);
        return RedirectToAction(actionName: "ViewCart", controllerName: "Cart");
    }

    public IActionResult BuyNow()
    {
        long userId = Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);
        var cartItems = _iCartBussiness.GetCartItems(userId);
        long totalAmount = 0;
        foreach (var item in cartItems)
        {
            totalAmount += item.Quantity * (Convert.ToInt64(item.Product.Price));
        }
        long orderDetailId = _iCartBussiness.BuyNow(userId, totalAmount.ToString());

        foreach (var item in cartItems)
        {
            OrderReceived orderReceived = new OrderReceived()
            {
                OrderDetailId = orderDetailId,
                ProductId = item.ProductId,
                UserId = item.UserId,
                Quantity = item.Quantity,
                Price = Convert.ToInt32(item.Product.Price),
                IsActive = item.IsActive,
                IsDeleted = item.IsDeleted,
                CreatedBy = userId,
                CreatedDate = DateTime.UtcNow
            };
            _iCartBussiness.AddOrderReceived(orderReceived);
            _iCartBussiness.DeleteItem(item.Id);
        }
        return RedirectToAction(actionName: "Payment", controllerName: "Cart", new { orderId = orderDetailId });
    }

    public IActionResult Payment(long orderId)
    {
        // if(!_iCartBussiness.CheckPaymentIdExists(orderId))
        //     return RedirectToAction(actionName:"Menu" , controllerName: "Home");
        return View(_iCartBussiness.GetOrderDetail(orderId));
    }

    public IActionResult OrderReceived()
    {
        var cartItems = _iCartBussiness.GetSuccessOrders();
        return View(cartItems);
    }

    public ActionResult PaymentWithPaypal(string Cancel = null, long id = 0, string PayerID = "", string guid = "")
    {
        var ClientID = _configuration.GetValue<string>("PayPal:Key");
        var ClientSecret = _configuration.GetValue<string>("PayPal:Secret");
        var mode = _configuration.GetValue<string>("PayPal:mode");
        APIContext apiContext = PaypalConfiguration.GetAPIContext(ClientID, ClientSecret, mode);
        try
        {
            string payerId = PayerID;
            if (string.IsNullOrEmpty(payerId))
            {
                string baseURI = this.Request.Scheme + "://" + this.Request.Host + "/Cart/PaymentWithPayPal?";
                var guidd = Convert.ToString((new Random()).Next(100000));
                guid = guidd;
                var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid + "&id=" + id, id);
                var links = createdPayment.links.GetEnumerator();
                string paypalRedirectUrl = null;
                while (links.MoveNext())
                {
                    Links lnk = links.Current;
                    if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                    {
                        paypalRedirectUrl = lnk.href;
                    }
                }
              
                httpContextAccessor.HttpContext.Session.SetString("payment", createdPayment.id);
                return Redirect(paypalRedirectUrl);
            }
            else
            {
                var paymentId = httpContextAccessor.HttpContext.Session.GetString("payment");
                var executedPayment = ExecutePayment(apiContext, payerId, paymentId as string);
                if (executedPayment.state.ToLower() != "approved")
                {
                    _iCartBussiness.UpdateOrderDetailStatusId(id, Convert.ToInt32(Common.OrderStatus.Failure));

                    return View("PaymentFailed");
                }
                _iCartBussiness.UpdateOrderDetailStatusId(id, Convert.ToInt32(Common.OrderStatus.Success));
                return RedirectToAction(nameof(MyOrders));

            }
        }
        catch (Exception ex)
        {
            return View("PaymentFailed");
        }
        return View("SuccessView");
    }
    public IActionResult MyOrders()
    {
        var myOrders = _iCartBussiness.GetSuccessOrders(Convert.ToInt64(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value));
        return View("PaymentSuccess", myOrders);
    }
    public IActionResult MarkAsInTransit(long orderDetailId)
    {
        _iCartBussiness.UpdateOrderDetailStatusId(orderDetailId, Convert.ToInt32(Common.OrderStatus.InTransit));
        return RedirectToAction(nameof(OrderReceived));
    }
    public IActionResult MarkAsDelivered(long orderDetailId)
    {
        _iCartBussiness.UpdateOrderDetailStatusId(orderDetailId, Convert.ToInt32(Common.OrderStatus.Delivered));
        return RedirectToAction(nameof(OrderReceived));
    }
    private PayPal.Api.Payment payment;
    private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
    {
        var paymentExecution = new PaymentExecution()
        {
            payer_id = payerId
        };
        this.payment = new Payment()
        {
            id = paymentId
        };
        return this.payment.Execute(apiContext, paymentExecution);
    }
    private Payment CreatePayment(APIContext apiContext, string redirectUrl, long orderDetailId)
    {
        var itemList = new ItemList()
        {
            items = new List<Item>()
        };
        var orderDetail = _iCartBussiness.GetOrderDetail(orderDetailId);
        foreach (var item in orderDetail)
        {
            itemList.items.Add(new Item()
            {
                name = item.Product.Name,
                currency = "USD",
                price = Convert.ToDouble(item.Price).ToString(),
                quantity = item.Quantity.ToString(),
                sku = "asd"
            });
        }

        var payer = new Payer()
        {
            payment_method = "paypal"
        };
        var redirUrls = new RedirectUrls()
        {
            cancel_url = redirectUrl + "&Cancel=true",
            return_url = redirectUrl
        };
       
        var amount = new Amount()
        {
            currency = "USD",
            total = Convert.ToDouble(orderDetail.FirstOrDefault().OrderDetail.BillValue).ToString(),
        };

        var transactionList = new List<Transaction>();
        transactionList.Add(new Transaction()
        {
            description = "Transaction description",
            invoice_number = Guid.NewGuid().ToString(),
            amount = amount,
            item_list = itemList
        });
        this.payment = new Payment()
        {
            intent = "sale",
            payer = payer,
            transactions = transactionList,
            redirect_urls = redirUrls
        };
       
        return this.payment.Create(apiContext);
    }


}