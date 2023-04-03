using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
namespace NS.FoodOrder.Business
{
    public interface ICategoryBussiness
    {
        bool ActivateDeactivateCategory(int Id);
        bool AddEditCategory(AddEditCategoryViewModel addEditCategoryViewModel);

        List<Category> GetCategoryList();

        AddEditCategoryViewModel GetCategoryById(int id);
    }
}