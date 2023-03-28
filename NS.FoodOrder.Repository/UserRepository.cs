using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
namespace NS.FoodOrder.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly FoodOrderDBContext _ctx;
        public UserRepository(FoodOrderDBContext ctx){
            _ctx=ctx;
        }

        public User GetUserDetailsByEmail(string email)
        {
            return _ctx.Users.Where(x => x.Email == email).FirstOrDefault();
        }

        public bool AddUser(Customer customer){
            
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


            _ctx.Add(user);

            _ctx.SaveChanges();
        
        return true;
        }

         public bool VerifyEmail(string email){
           return _ctx.Users.Any(x=>x.Email==email);
         }
    }
}