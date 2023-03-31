using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
namespace NS.FoodOrder.Repository
{
    public interface IUserRepository
    {
        User GetUserDetailsByEmail(string email);
        bool AddUser(Customer customer);
        bool VerifyEmail(string email);
        public List<User> GetUserList(string Sorting_Order, string Search_Data);
        bool ActivateDeactivateRecord(int Id);
        bool ActivateDeactivateCategory(int Id);

        bool AddContactDetails(ContactViewModel contactViewModel);

        bool AddEditCategory(Category category);
        List<Category> GetCategoryList();

        AddEditCategoryViewModel GetCategoryById(int id);

        bool AddEditProduct(Product product);
        bool ActivateDeactivateEligible(int Id);
         bool ActivateDeactivateProduct(int Id);

        List<Product> GetProductList();

        AddEditProductViewModel GetProductById(int id);
    }
}