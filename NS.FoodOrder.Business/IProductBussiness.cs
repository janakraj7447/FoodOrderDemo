using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
namespace NS.FoodOrder.Business
{
    public interface IProductBussiness
    {
        bool AddEditProduct(AddEditProductViewModel addEditProductViewModel);
        List<Product> GetProductList();
        AddEditProductViewModel GetProductById(int id);
        List<Product> GetProductByCategoryId(int categoryId);
        bool ActivateDeactivateEligible(int Id);

        bool ActivateDeactivateProduct(int id);

        bool AddToCart(CartViewModel cartViewModel);
        List<Cart> GetCartItems(long userId);
        bool DeleteItem(int Id);
        bool AddQuantity(int productId, long userId);
        bool SubtractQuantity(int productId, long userId);

    }
}