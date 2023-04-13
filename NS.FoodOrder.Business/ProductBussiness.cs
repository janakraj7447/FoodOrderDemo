using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
using NS.FoodOrder.Repository;
namespace NS.FoodOrder.Business
{
    public class ProductBussiness : IProductBussiness
    {
        public readonly IProductRepository _iProductRepository;
        public ProductBussiness(IProductRepository iProductRepository)
        {
            _iProductRepository = iProductRepository;

        }
        public bool AddEditProduct(AddEditProductViewModel addEditProductViewModel)
        {
            return _iProductRepository.AddEditProduct(new Product()
            {
                Id = addEditProductViewModel.Id,
                Name = addEditProductViewModel.Name,
                Price = addEditProductViewModel.Price,
                CategoryId = addEditProductViewModel.CategoryId,
                Description = addEditProductViewModel.Description,
                Photo = addEditProductViewModel.Photo,
                IsEligibleForDiscount = true,
                IsActive = true,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = addEditProductViewModel.CreatedBy
            });
        }

        public AddEditProductViewModel GetProductById(int id)
        {
            return _iProductRepository.GetProductById(id);
        }

        public List<Product> GetProductList()
        {
            return _iProductRepository.GetProductList();
        }
        public bool ActivateDeactivateEligible(int Id)
        {
            return _iProductRepository.ActivateDeactivateEligible(Id);
        }
        public bool ActivateDeactivateProduct(int Id)
        {
            return _iProductRepository.ActivateDeactivateProduct(Id);
        }

        public List<Product> GetProductByCategoryId(int categoryId)
        {
            return _iProductRepository.GetProductByCategoryId(categoryId);
        }
      

}
}