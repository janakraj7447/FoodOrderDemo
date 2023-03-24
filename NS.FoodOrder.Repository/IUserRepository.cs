using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Models;
namespace NS.FoodOrder.Repository
{
    public interface IUserRepository
    {
         User LoginPage(Customer customer);
         bool AddUser(Customer customer);
    }
}