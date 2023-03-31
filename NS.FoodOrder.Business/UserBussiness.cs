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

        public bool AddEditCategory(AddEditCategoryViewModel addEditCategoryViewModel)
        {
            return _iUserRepository.AddEditCategory(new Category()
            {
                Id = addEditCategoryViewModel.Id,
                Name = addEditCategoryViewModel.Name,
                IsActive = true,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = addEditCategoryViewModel.CreatedBy
            });
        }
        public AddEditCategoryViewModel GetCategoryById(int id)
        {
            return _iUserRepository.GetCategoryById(id);
        }

        public List<Category> GetCategoryList()
        {
            return _iUserRepository.GetCategoryList();
        }

        public bool ActivateDeactivateCategory(int Id)
        {
            return _iUserRepository.ActivateDeactivateCategory(Id);
        }

          public bool AddEditProduct(AddEditProductViewModel addEditProductViewModel)
        {
            return _iUserRepository.AddEditProduct(new Product()
            {
                Id = addEditProductViewModel.Id,
                Name = addEditProductViewModel.Name,
                Price=addEditProductViewModel.Price,
                CategoryId=addEditProductViewModel.CategoryId,
                Description=addEditProductViewModel.Description,
                Photo=addEditProductViewModel.Photo,
                IsEligibleForDiscount=true,
                IsActive = true,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = addEditProductViewModel.CreatedBy
            });
        }

          public AddEditProductViewModel GetProductById(int id)
        {
            return _iUserRepository.GetProductById(id);
        }

         public List<Product> GetProductList()
        {
            return _iUserRepository.GetProductList();
        }
        public bool ActivateDeactivateEligible(int Id)
        {
            return _iUserRepository.ActivateDeactivateEligible(Id);
        }
           public bool ActivateDeactivateProduct(int Id)
        {
            return _iUserRepository.ActivateDeactivateProduct(Id);
        }
    }
}