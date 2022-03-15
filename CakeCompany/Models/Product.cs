namespace CakeCompany.Models;

public class Product
{
    public Guid Id { get; set; }
    public CakeEnum CakeType { get; set; }
    public double Quantity { get; set; }

    //NOTE: this property is inclongruous. 
    //public int OrderId { get; set; }
}
