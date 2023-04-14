namespace NS.FoodOrder.Data.CustomEntities
{
    public static class Common
    {
        public enum Role
        {
            Admin = 1,
            User = 2
        }
        public enum OrderStatus{
            Pending=1,
            Success=2,
            Failure=3,
            InTransit=4,
            Delivered=5,
            Cancelled=6
        }
    }

}