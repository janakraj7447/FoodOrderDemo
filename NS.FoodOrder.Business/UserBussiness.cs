using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Models;
using NS.FoodOrder.Repository;
namespace NS.FoodOrder.Business
{
    public class UserBussiness:IUserBussiness
    {
        public readonly IUserRepository _iUserRepository;
        public UserBussiness(IUserRepository iUserRepository){
           _iUserRepository=iUserRepository; 
      
        }
         public User LoginPage(Customer customer){
            return _iUserRepository.LoginPage(customer);
         }

           public bool AddUser(Customer customer){
            return _iUserRepository.AddUser(customer);
           }

    }
}