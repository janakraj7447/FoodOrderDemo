using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
namespace NS.FoodOrder.Business
{
    public interface IProductBussiness
    {
        bool AddEditProduct(AddEditProductViewModel addEditProductViewModel);
        List<Product> GetProductList();

        AddEditProductViewModel GetProductById(int id);
        bool ActivateDeactivateEligible(int Id);

        bool ActivateDeactivateProduct(int id);
    }
}