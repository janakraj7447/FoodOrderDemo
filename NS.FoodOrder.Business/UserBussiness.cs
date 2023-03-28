using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
using NS.FoodOrder.Repository;
namespace NS.FoodOrder.Business
{
    public class UserBussiness:IUserBussiness
    {
        public readonly IUserRepository _iUserRepository;
        public UserBussiness(IUserRepository iUserRepository){
           _iUserRepository=iUserRepository; 
      
        }
         public User GetUserDetailsByEmail(string email){
            return _iUserRepository.GetUserDetailsByEmail(email);
         }

           public bool AddUser(Customer customer){
            return _iUserRepository.AddUser(customer);
           }

          public bool VerifyEmail(string email){
            return _iUserRepository.VerifyEmail(email);
          }

    }
}