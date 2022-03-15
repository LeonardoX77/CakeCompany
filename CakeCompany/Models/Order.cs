namespace CakeCompany.Models;

public class Order
{
    public int Id { get; set; }
    public string ClientName { get; set; }
    public DateTime EstimatedDeliveryTime { get; set; }
    public CakeEnum CakeType { get; set; }
    public double Quantity { get; set; }

    public Order(string clientName, DateTime estimatedDeliveryTime, int id, CakeEnum cakeType, double quantity)
    {
        Id = id;
        ClientName = clientName;
        EstimatedDeliveryTime = estimatedDeliveryTime;
        CakeType = cakeType;
        Quantity = quantity;
    }

}
