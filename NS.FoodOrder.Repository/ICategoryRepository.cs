using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
namespace NS.FoodOrder.Repository
{
    public interface ICategoryRepository
    {
        bool ActivateDeactivateCategory(int Id);
        bool AddEditCategory(Category category);
        List<Category> GetCategoryList();
        AddEditCategoryViewModel GetCategoryById(int id);
    }
}