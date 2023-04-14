using System.ComponentModel.DataAnnotations;
namespace NS.FoodOrder.Data.CustomEntities
{
    public class AddEditCategoryViewModel
    {
        public long Id { get; set; }
        [RegularExpression("^[a-zA-Z ]{0,50}$", ErrorMessage = "Please Enter Valid Name")]
        public string Name { get; set; }
        public long CreatedBy { get; set; }

    }
}