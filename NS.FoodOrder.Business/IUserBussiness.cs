using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
namespace NS.FoodOrder.Business
{
    public interface IUserBussiness
    {
        User GetUserDetailsByEmail(string email);
        bool AddUser(Customer customer);
        bool VerifyEmail(string email);


        List<User> GetUserList(string Sorting_Order, string Search_Data);
        bool ActivateDeactivateRecord(int Id);
        bool ActivateDeactivateCategory(int Id);

        bool AddContactDetails(ContactViewModel contactViewModel);

        bool AddEditCategory(AddEditCategoryViewModel addEditCategoryViewModel);

        List<Category> GetCategoryList();

        AddEditCategoryViewModel GetCategoryById(int id);

         bool ActivateDeactivateEligible(int Id);

        bool ActivateDeactivateProduct(int id);

         bool AddEditProduct(AddEditProductViewModel addEditProductViewModel);
        List<Product> GetProductList();

        AddEditProductViewModel GetProductById(int id);
    }
}