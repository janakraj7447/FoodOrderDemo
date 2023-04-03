using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
using NS.FoodOrder.Repository;
using NS.FoodOrder.Business;

namespace NS.FoodOrder.Business
{
    public class CategoryBussiness : ICategoryBussiness
    {
        public readonly ICategoryRepository _iCategoryRepository;
        public CategoryBussiness(ICategoryRepository iCategoryRepository)
        {
            _iCategoryRepository = iCategoryRepository;

        }
        public bool AddEditCategory(AddEditCategoryViewModel addEditCategoryViewModel)
        {
            return _iCategoryRepository.AddEditCategory(new Category()
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
            return _iCategoryRepository.GetCategoryById(id);
        }

        public List<Category> GetCategoryList()
        {
            return _iCategoryRepository.GetCategoryList();
        }

        public bool ActivateDeactivateCategory(int Id)
        {
            return _iCategoryRepository.ActivateDeactivateCategory(Id);
        }
    }
}