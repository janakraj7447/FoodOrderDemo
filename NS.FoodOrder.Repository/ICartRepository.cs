using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
namespace NS.FoodOrder.Repository
{
    public interface ICartRepository
    {
        bool AddToCart(CartViewModel cartViewModel);
        bool DeleteItem(long Id);
        List<Cart> GetCartItems(long userId);
        bool AddQuantity(int productId, long userId);
        bool SubtractQuantity(int productId, long userId);
        long BuyNow(long userId, string BillValue);
        bool AddOrderReceived(OrderReceived orderReceived);
        bool CheckPaymentIdExists(long orderDetailId);
        List<OrderReceived> GetOrderDetail(long orderDetailId);
        List<OrderDetail> GetSuccessOrders();
        bool UpdateOrderDetailStatusId(long orderDetailId, int statusId);
    }
}