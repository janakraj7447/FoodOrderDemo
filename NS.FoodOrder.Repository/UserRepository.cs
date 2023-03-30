using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
namespace NS.FoodOrder.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly FoodOrderDBContext _ctx;
        public UserRepository(FoodOrderDBContext ctx)
        {
            _ctx = ctx;
        }

        public User GetUserDetailsByEmail(string email)
        {
            return _ctx.Users.FirstOrDefault(x => x.Email == email);
        }

        public bool AddUser(Customer customer)
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
            user.RoleId = Convert.ToInt64(Common.Role.User);
            user.PhoneNo = customer.PhoneNo;
            user.Email = customer.Email;
            user.Password = customer.Password;
            user.CreatedBy = user.Id;
            user.CreatedDate = DateTime.Now;
            user.IsVerified = true;
            user.IsActive = true;


            _ctx.Add(user);

            _ctx.SaveChanges();

            return true;
        }

        public bool VerifyEmail(string email)
        {
            return _ctx.Users.Any(x => x.Email == email);
        }

        public List<User> GetUserList(string Sorting_Order, string Search_Data)
        {


            var userAccount = from stu in _ctx.Users.Where(x=>x.RoleId==2) select stu;
           
            //if search box does not empty then this will run
            if (!string.IsNullOrEmpty(Search_Data))
            {
                userAccount = userAccount.Where(stu => stu.FirstName.Contains(Search_Data));
            }
            switch (Sorting_Order)
            {
                case "Name_Description":
                    userAccount = userAccount.OrderByDescending(stu => stu.FirstName);
                    break;
                case "Date_Enroll":
                    userAccount = userAccount.OrderBy(stu => stu.Age);
                    break;
                case "Date_Description":
                    userAccount = userAccount.OrderByDescending(stu => stu.Age);
                    break;
                default:
                    userAccount = userAccount.OrderBy(stu => stu.FirstName);
                    break;
            }
            // return _ctx.Users.ToList();
            return userAccount.ToList();

        }

          public  List<Category> GetCategoryList(){
            return _ctx.Categories.ToList();
         }

        public bool ActivateDeactivateRecord(int Id)
        {
            var candidateRecord = _ctx.Users.FirstOrDefault(x => x.Id == Id);
            if (candidateRecord != null)
            {
                candidateRecord.IsActive = !candidateRecord.IsActive;
                _ctx.SaveChanges();

            }
            return true;

        }

        public bool AddContactDetails(ContactViewModel contactViewModel)
        {
            ContactU contactUs = new ContactU();
            contactUs.Name = contactViewModel.Name;
            contactUs.Email = contactViewModel.Email;
            contactUs.Subject = contactViewModel.Subject;
            contactUs.Description = contactViewModel.Description;
            contactUs.CreatedBy = contactViewModel.Id;
            contactUs.CreatedDate = DateTime.Now;

            _ctx.ContactUs.Add(contactUs);

            _ctx.SaveChanges();

            return true;
        }

        public bool AddCategory(CategoryViewModel categoryViewModel){
            Category category=new Category();
            category.Name=categoryViewModel.Name;
            category.CreatedBy =categoryViewModel.Id;
            category.CreatedDate = DateTime.Now;
            _ctx.Add(category);
            _ctx.SaveChanges();
            return true;
         }

          public bool ActivateDeactivateCategory(int Id)
        {
            var candidateRecord = _ctx.Categories.FirstOrDefault(x => x.Id == Id);
            if (candidateRecord != null)
            {
                candidateRecord.IsActive = !candidateRecord.IsActive;
                _ctx.SaveChanges();

            }
            return true;

        }

       


    }
}