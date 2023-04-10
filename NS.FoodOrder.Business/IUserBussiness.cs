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
        List<ContactU> GetContactList();
        bool ActivateDeactivateRecord(int Id);
      
        bool AddContactDetails(ContactViewModel contactViewModel);

    }
}