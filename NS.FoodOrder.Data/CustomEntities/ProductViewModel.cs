namespace NS.FoodOrder.Data.CustomEntities
{
    public class ProductViewModel
    {
        public long Id { get; set; }

        public long CategoryId { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Description { get; set; }

        public bool IsEligibleForDiscount { get; set; }

        public string Photo { get; set; }

        public bool? IsActive { get; set; }


        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }


    }
}