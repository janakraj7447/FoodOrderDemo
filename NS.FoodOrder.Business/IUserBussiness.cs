using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Models;
namespace NS.FoodOrder.Business
{
    public interface IUserBussiness
    {
          User LoginPage(Customer customer); 
          bool AddUser(Customer customer);
    }
}