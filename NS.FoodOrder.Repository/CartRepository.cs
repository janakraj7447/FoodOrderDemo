using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
using Microsoft.EntityFrameworkCore;
namespace NS.FoodOrder.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly FoodOrderDBContext _ctx;
        public CartRepository(FoodOrderDBContext ctx)
        {
            _ctx = ctx;
        }
        public bool AddToCart(CartViewModel cartViewModel)
        {
            if (_ctx.Carts.Any(x => x.ProductId == cartViewModel.ProductId && x.UserId == cartViewModel.UserId))
            {
                Cart cart = _ctx.Carts.FirstOrDefault(x => x.ProductId == cartViewModel.ProductId && x.UserId == cartViewModel.UserId);
                cart.Quantity = cart.Quantity + 1;
                return _ctx.SaveChanges() > 0;
            }
            else
            {
                Cart cart = new Cart();
                cart.ProductId = cartViewModel.ProductId;
                cart.UserId = cartViewModel.UserId;
                cart.Quantity = 1;
                cart.CreatedBy = cartViewModel.UserId;
                cart.CreatedDate = DateTime.UtcNow;
                _ctx.Carts.Add(cart);
                return _ctx.SaveChanges() > 0;
            }
        }

        public List<Cart> GetCartItems(long userId)
        {
            var cartItems = _ctx.Carts.Include("Product").Include("User").Where(x => x.UserId == userId).ToList();
            return cartItems;
        }

        public bool DeleteItem(long Id)
        {
            var item = _ctx.Carts.FirstOrDefault(x => x.Id == Id);
            if (item != null)
            {
                _ctx.Carts.Remove(item);
                _ctx.SaveChanges();

            }
            return true;
        }
        public bool AddQuantity(int productId, long userId)
        {
            if (_ctx.Carts.Any(x => x.ProductId == productId && x.UserId == userId))
            {
                Cart cart = _ctx.Carts.FirstOrDefault(x => x.ProductId == productId && x.UserId == userId);
                cart.Quantity = cart.Quantity + 1;
                return _ctx.SaveChanges() > 0;
            }
            return false;
        }
        public bool SubtractQuantity(int productId, long userId)
        {
            if (_ctx.Carts.Any(x => x.ProductId == productId && x.UserId == userId))
            {
                Cart cart = _ctx.Carts.FirstOrDefault(x => x.ProductId == productId && x.UserId == userId);
                cart.Quantity = cart.Quantity - 1;
                return _ctx.SaveChanges() > 0;
            }
            return false;
        }

        public long BuyNow(long userId, string BillValue)
        {
            OrderDetail orderDetail = new OrderDetail();
            orderDetail.BillValue = BillValue;
            orderDetail.PaymentModeId = 2;
            orderDetail.IsActive = true;
            orderDetail.IsDeleted = false;
            orderDetail.CreatedDate = DateTime.Now;
            orderDetail.CreatedBy = userId;
            orderDetail.StatusId = Convert.ToInt32(Common.OrderStatus.Pending);
            _ctx.Add(orderDetail);
            _ctx.SaveChanges();

            return orderDetail.Id;
        }
        public bool AddOrderReceived(OrderReceived orderReceived)
        {
            _ctx.Add(orderReceived);
            return _ctx.SaveChanges() > 0;
        }

        public bool CheckPaymentIdExists(long orderDetailId)
        {
            return _ctx.OrderDetails.Any(x => x.PaymentModeId == null && x.Id == orderDetailId);
        }

        public List<OrderReceived> GetOrderDetail(long orderDetailId)
        {
            return _ctx.OrderReceiveds.Include("OrderDetail").Include("Product").Where(x => x.OrderDetailId == orderDetailId).ToList();

        }
        public List<OrderDetail> GetSuccessOrders()
        {
          return _ctx.OrderDetails.Include("OrderReceived").Include("Product").Include("User").Where(x => x.StatusId == Convert.ToInt32(Common.OrderStatus.Success)).ToList();
        }
        public bool UpdateOrderDetailStatusId(long orderDetailId, int statusId)
        {
            if (_ctx.OrderDetails.Any(x => x.Id == orderDetailId))
            {
                OrderDetail orderDetail = _ctx.OrderDetails.FirstOrDefault(x => x.Id == orderDetailId);
                orderDetail.StatusId = statusId;
                return _ctx.SaveChanges() > 0;
            }
            return false;
        }


    }
}