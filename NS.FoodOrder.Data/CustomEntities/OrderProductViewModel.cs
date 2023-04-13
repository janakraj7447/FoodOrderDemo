
using NS.FoodOrder.Data.Entities;
namespace NS.FoodOrder.Data.CustomEntities{
public class OrderProductViewModel{
 
  public OrderProductViewModel(){
    Products=new List<Product>();
  }
   public long Id { get; set; }

    public string BillValue { get; set; }

    public int Quantity{get;set;}
    // public string Name{get;set;}
     public List<Product> Products{ get; set; }
}
}