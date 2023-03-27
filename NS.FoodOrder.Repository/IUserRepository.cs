using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
namespace NS.FoodOrder.Repository
{
    public interface IUserRepository
    {
         User LoginPage(Customer customer);
         bool AddUser(Customer customer);
         bool VerifyEmail(string email);
    }
}