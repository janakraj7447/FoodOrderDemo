using NS.FoodOrder.Data.Entities;
using NS.FoodOrder.Data.CustomEntities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace NS.FoodOrder.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly FoodOrderDBContext _ctx;
        public ProductRepository(FoodOrderDBContext ctx)
        {
            _ctx = ctx;
        }
        public bool AddEditProduct(Product product)
        {
            if (product.Id > 0)
            {
                var prod = _ctx.Products.FirstOrDefault(x => x.Id == product.Id);
                prod.Name = product.Name;
                prod.Price = product.Price;
                prod.CategoryId = product.CategoryId;
                prod.Description = product.Description;
                prod.UpdatedBy = product.CreatedBy;
                prod.Updateddate = DateTime.UtcNow;
                return _ctx.SaveChanges() > 0;

            }
            else
            {
                _ctx.Add(product);
                return _ctx.SaveChanges() > 0;
            }
        }

        public List<Product> GetProductList()
        {
            return _ctx.Products.Include("Category").ToList();
        }


        public AddEditProductViewModel GetProductById(int id)
        {
            var product = _ctx.Products.FirstOrDefault(x => x.Id == id);
            return new AddEditProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Price = product.Price,
                Description = product.Description,
                Photo = product.Photo
            };
        }

        public bool ActivateDeactivateEligible(int Id)
        {
            var product = _ctx.Products.FirstOrDefault(x => x.Id == Id);
            if (product != null)
            {
                product.IsEligibleForDiscount = !product.IsEligibleForDiscount;
                _ctx.SaveChanges();

            }
            return true;

        }
        public bool ActivateDeactivateProduct(int Id)
        {
            var product = _ctx.Products.FirstOrDefault(x => x.Id == Id);
            if (product != null)
            {
                product.IsActive = !product.IsActive;
                _ctx.SaveChanges();

            }
            return true;

        }


    }
}