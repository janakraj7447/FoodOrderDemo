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
        public List<Product> GetProductByCategoryId(int categoryId)
        {
            List<Product> productList = new List<Product>();
            if (categoryId > 0)
            {
                productList = _ctx.Products.Where(x => x.CategoryId == categoryId).ToList();
            }
            else
            {
                productList = _ctx.Products.ToList();
            }
            return productList;

        }
        public bool AddToCart(CartViewModel cartViewModel)
        {
            Cart cart = new Cart();
            cart.ProductId = cartViewModel.ProductId;
            cart.UserId = cartViewModel.UserId;
            cart.Quantity = 1;
            cart.CreatedBy = cartViewModel.Id;
            cart.CreatedDate = DateTime.UtcNow;
            _ctx.Carts.Add(cart);
            _ctx.SaveChanges();
            return true;
        }

        public List<Cart> GetCartItems(long userId){
         var cartItems=_ctx.Carts.Include("Product").Include("User").Where(x=>x.UserId==userId).ToList();
         return cartItems;
        }
        
        public bool DeleteItem(int Id)
        {
            var item = _ctx.Carts.FirstOrDefault(x => x.Id == Id);
            if (item != null)
            {
                _ctx.Carts.Remove(item);
                _ctx.SaveChanges();

            }
            return true;
        }

    }
}