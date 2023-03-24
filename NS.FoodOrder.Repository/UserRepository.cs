using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Models;
namespace NS.FoodOrder.Repository
{
    public class UserRepository : IUserRepository
    {
        public User LoginPage(Customer customer)
        {
            using (var context = new FoodOrderDBContext())
            {
              return context.Users.Where(x => x.Email == customer.Email).FirstOrDefault();
            }
        }

        public bool AddUser(Customer customer){
             using (var context = new FoodOrderDBContext())
        {
            User user = new User();
            user.FirstName = customer.FirstName;
            user.LastName = customer.LastName;
            user.ProfilePic = customer.ProfilePic;
            user.Age = customer.Age;
            user.Address = customer.Address;
            user.City = customer.City;
            user.State = customer.State;
            user.Country = customer.Country;
            user.PinCode = customer.PinCode;
            user.RoleId= Convert.ToInt64(Common.Role.User);
            user.PhoneNo = customer.PhoneNo;
            user.Email = customer.Email;
            user.Password = customer.Password;
            user.CreatedBy = user.Id;
            user.CreatedDate = DateTime.Today;
            user.IsVerified = true;
            user.IsActive=true;


            context.Add(user);

            context.SaveChanges();
        }
        return true;
        }
    }
}