using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
using NS.FoodOrder.Repository;
namespace NS.FoodOrder.Business
{
    public class CartBussiness : ICartBussiness
    {
        public readonly ICartRepository _iCartRepository;
        public CartBussiness(ICartRepository iCartRepository)
        {
            _iCartRepository = iCartRepository;

        }
        public bool AddToCart(CartViewModel cartViewModel)
        {
            return _iCartRepository.AddToCart(cartViewModel);
        }
        public List<Cart> GetCartItems(long userId)
        {
            return _iCartRepository.GetCartItems(userId);
        }
        public bool DeleteItem(long Id)
        {
            return _iCartRepository.DeleteItem(Id);
        }
        public bool AddQuantity(int productId, long userId)
        {
            return _iCartRepository.AddQuantity(productId, userId);
        }
        public bool SubtractQuantity(int productId, long userId)
        {
            return _iCartRepository.SubtractQuantity(productId, userId);
        }
        public long BuyNow(long userId, string BillValue)
        {
            return _iCartRepository.BuyNow(userId, BillValue);
        }
        public bool AddOrderReceived(OrderReceived orderReceived)
        {
            return _iCartRepository.AddOrderReceived(orderReceived);
        }
        public bool CheckPaymentIdExists(long orderDetailId)
        {
            return _iCartRepository.CheckPaymentIdExists(orderDetailId);
        }
        public List<OrderReceived> GetOrderDetail(long orderDetailId)
        {
            return _iCartRepository.GetOrderDetail(orderDetailId);
        }
        public List<OrderReceived> GetSuccessOrders(long userId = 0)
        {
            return _iCartRepository.GetSuccessOrders(userId);
        }
        public bool UpdateOrderDetailStatusId(long orderDetailId, int statusId)
        {
            return _iCartRepository.UpdateOrderDetailStatusId(orderDetailId, statusId);
        }
    }
}