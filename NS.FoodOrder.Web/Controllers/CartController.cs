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
        //getting the apiContext
        var ClientID = _configuration.GetValue<string>("PayPal:Key");
        var ClientSecret = _configuration.GetValue<string>("PayPal:Secret");
        var mode = _configuration.GetValue<string>("PayPal:mode");
        APIContext apiContext = PaypalConfiguration.GetAPIContext(ClientID, ClientSecret, mode);
        // apiContext.AccessToken="Bearer access_token$production$j27yms5fthzx9vzm$c123e8e154c510d70ad20e396dd28287";
        try
        {
            //A resource representing a Payer that funds a payment Payment Method as paypal
            //Payer Id will be returned when payment proceeds or click to pay
            string payerId = PayerID;
            if (string.IsNullOrEmpty(payerId))
            {
                //this section will be executed first because PayerID doesn't exist
                //it is returned by the create function call of the payment class
                // Creating a payment
                // baseURL is the url on which paypal sendsback the data.
                string baseURI = this.Request.Scheme + "://" + this.Request.Host + "/Cart/PaymentWithPayPal?";
                //here we are generating guid for storing the paymentID received in session
                //which will be used in the payment execution
                var guidd = Convert.ToString((new Random()).Next(100000));
                guid = guidd;
                //CreatePayment function gives us the payment approval url
                //on which payer is redirected for paypal account payment
                var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid + "&id=" + id, id);
                //get links returned from paypal in response to Create function call
                var links = createdPayment.links.GetEnumerator();
                string paypalRedirectUrl = null;
                while (links.MoveNext())
                {
                    Links lnk = links.Current;
                    if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                    {
                        //saving the payapalredirect URL to which user will be redirected for payment
                        paypalRedirectUrl = lnk.href;
                    }
                }
                // saving the paymentID in the key guid
                httpContextAccessor.HttpContext.Session.SetString("payment", createdPayment.id);
                return Redirect(paypalRedirectUrl);
            }
            else
            {
                // This function exectues after receving all parameters for the payment
                var paymentId = httpContextAccessor.HttpContext.Session.GetString("payment");
                var executedPayment = ExecutePayment(apiContext, payerId, paymentId as string);
                //If executed payment failed then we will show payment failure message to user
                if (executedPayment.state.ToLower() != "approved")
                {
                    _iCartBussiness.UpdateOrderDetailStatusId(id, Convert.ToInt32(Common.OrderStatus.Failure));
                 
                    return View("PaymentFailed");
                }
                _iCartBussiness.UpdateOrderDetailStatusId(id, Convert.ToInt32(Common.OrderStatus.Success));
             

                var blogIds = executedPayment.transactions[0].item_list.items[0].sku;
                return View("PaymentSuccess");
            }
        }
        catch (Exception ex)
        {
            return View("PaymentFailed");
        }
        //on successful payment, show success page to user.
        return View("SuccessView");
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
        //create itemlist and add item objects to it
        var itemList = new ItemList()
        {
            items = new List<Item>()
        };
        var orderDetail = _iCartBussiness.GetOrderDetail(orderDetailId);
        foreach (var item in orderDetail)
        {
            //Adding Item Details like name, currency, price etc
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
        // Configure Redirect Urls here with RedirectUrls object
        var redirUrls = new RedirectUrls()
        {
            cancel_url = redirectUrl + "&Cancel=true",
            return_url = redirectUrl
        };
        // Adding Tax, shipping and Subtotal details
        //var details = new Details()
        //{
        //    tax = "1",
        //    shipping = "1",
        //    subtotal = "1"
        //};
        //Final amount with details
        var amount = new Amount()
        {
            currency = "USD",
            total = Convert.ToDouble(orderDetail.FirstOrDefault().OrderDetail.BillValue).ToString(), // Total must be equal to sum of tax, shipping and subtotal.
                                                                                                     //details = details
        };

        var transactionList = new List<Transaction>();
        // Adding description about the transaction
        transactionList.Add(new Transaction()
        {
            description = "Transaction description",
            invoice_number = Guid.NewGuid().ToString(), //Generate an Invoice No
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
        // Create a payment using a APIContext
        return this.payment.Create(apiContext);
    }


}