using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
namespace NS.FoodOrder.Business
{
    public interface IUserBussiness
    {
          User LoginPage(Customer customer); 
          bool AddUser(Customer customer);
          bool VerifyEmail(string email);
    }
}