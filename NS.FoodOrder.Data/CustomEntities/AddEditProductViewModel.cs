using System.ComponentModel.DataAnnotations;
namespace NS.FoodOrder.Data.CustomEntities
{
    public class AddEditProductViewModel
    {
        public long Id { get; set; }
        public long CategoryId { get; set; }
        [RegularExpression("^[a-zA-Z ]{0,50}$", ErrorMessage = "Please Enter Valid Name")]
        public string Name { get; set; }
        [RegularExpression(@"^([0-9]{0,5})$", ErrorMessage = "Please Enter Valid Price.")]
        public string Price { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsEligibleForDiscount { get; set; }
        public string Photo { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }


    }
}