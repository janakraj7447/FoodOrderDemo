using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
namespace NS.FoodOrder.Repository
{
    public interface IProductRepository
    {
        bool AddEditProduct(Product product);
        bool ActivateDeactivateEligible(int Id);
        bool ActivateDeactivateProduct(int Id);
        List<Product> GetProductList();
        AddEditProductViewModel GetProductById(int id);
    }
}