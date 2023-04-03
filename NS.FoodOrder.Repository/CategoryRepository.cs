using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
namespace NS.FoodOrder.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FoodOrderDBContext _ctx;
        public CategoryRepository(FoodOrderDBContext ctx)
        {
            _ctx = ctx;
        }
        public bool AddEditCategory(Category category)
        {
            if (category.Id > 0)
            {
                var cat = _ctx.Categories.FirstOrDefault(x => x.Id == category.Id);
                cat.Name = category.Name;
                cat.UpdatedBy = category.CreatedBy;
                cat.UpdatedDate = DateTime.UtcNow;
                return _ctx.SaveChanges() > 0;

            }
            else
            {
                _ctx.Add(category);
                return _ctx.SaveChanges() > 0;
            }
        }

        public bool ActivateDeactivateCategory(int Id)
        {
            var categoryRecord = _ctx.Categories.FirstOrDefault(x => x.Id == Id);
            if (categoryRecord != null)
            {
                categoryRecord.IsActive = !categoryRecord.IsActive;
                _ctx.SaveChanges();

            }
            return true;

        }
        public List<Category> GetCategoryList()
        {
            return _ctx.Categories.ToList();
        }

        public AddEditCategoryViewModel GetCategoryById(int id)
        {
            var category = _ctx.Categories.FirstOrDefault(x => x.Id == id);
            return new AddEditCategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}