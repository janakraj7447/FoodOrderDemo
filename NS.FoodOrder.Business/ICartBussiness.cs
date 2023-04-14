using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
namespace NS.FoodOrder.Business
{
    public interface ICartBussiness
    {
        bool AddToCart(CartViewModel cartViewModel);
        List<Cart> GetCartItems(long userId);
        bool DeleteItem(long Id);
        bool AddQuantity(int productId, long userId);
        bool SubtractQuantity(int productId, long userId);
        long BuyNow(long userId, string BillValue);
        bool AddOrderReceived(OrderReceived orderReceived);
        bool CheckPaymentIdExists(long orderDetailId);
        List<OrderReceived> GetOrderDetail(long orderDetailId);
        List<OrderReceived> GetSuccessOrders(long userId = 0);
        bool UpdateOrderDetailStatusId(long orderDetailId, int statusId);
    }
}