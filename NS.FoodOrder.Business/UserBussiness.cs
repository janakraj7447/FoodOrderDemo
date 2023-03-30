using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
using NS.FoodOrder.Repository;
namespace NS.FoodOrder.Business
{
    public class UserBussiness : IUserBussiness
    {
        public readonly IUserRepository _iUserRepository;
        public UserBussiness(IUserRepository iUserRepository)
        {
            _iUserRepository = iUserRepository;

        }
        public User GetUserDetailsByEmail(string email)
        {
            return _iUserRepository.GetUserDetailsByEmail(email);
        }

        public bool AddUser(Customer customer)
        {
            return _iUserRepository.AddUser(customer);
        }

        public bool VerifyEmail(string email)
        {
            return _iUserRepository.VerifyEmail(email);
        }

        public List<User> GetUserList(string Sorting_Order, string Search_Data)
        {
            return _iUserRepository.GetUserList(Sorting_Order, Search_Data);
        }

        public bool ActivateDeactivateRecord(int Id)
        {
            return _iUserRepository.ActivateDeactivateRecord(Id);
        }

        public bool AddContactDetails(ContactViewModel contactViewModel)
        {
            return _iUserRepository.AddContactDetails(contactViewModel);
        }

        public bool AddCategory(CategoryViewModel categoryViewModel)
        {
            return _iUserRepository.AddCategory(categoryViewModel);
        }
        public List<Category> GetCategoryList()
        {
            return _iUserRepository.GetCategoryList();
        }

        public bool ActivateDeactivateCategory(int Id)
        {
            return _iUserRepository.ActivateDeactivateCategory(Id);
        }
    }
}